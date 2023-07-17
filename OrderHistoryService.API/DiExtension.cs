using Delivery.BaseLib.Core.Filters;
using Delivery.BaseLib.Infrastructure.Transactions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderHistoryService.Application.Consumers;
using OrderHistoryService.Application.Services;
using OrderHistoryService.Domain.Events.DeliveryService;
using OrderHistoryService.Domain.Events.OrderService;
using OrderHistoryService.Domain.Events.SupplierService;
using OrderHistoryService.Infrastructure.Context;
using OrderHistoryService.Infrastructure.Repositories;

namespace OrderHistoryService.API;

public static class DiExtension
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(config => { config.Filters.Add<ExceptionFilter>(); });

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Delivery.OrderHistoryService.Infra")));

        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        services.Scan(scan => scan.FromAssemblyOf<OrderHistoryAppService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<OrderHistoryRepository>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<Domain.Services.OrderHistoryService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddMassTransit(bus =>
        {
            bus.AddRider(rider =>
            {
                rider.AddConsumer<OrderCreatedConsumer>();
                rider.AddConsumer<SupplierCancelledConsumer>();
                rider.AddConsumer<DeliveryStartedConsumer>();
                rider.AddConsumer<DeliveryFinishedConsumer>();
                rider.AddConsumer<SupplierFinishedConsumer>();

                rider.UsingKafka((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("Kafka"));

                    cfg.TopicEndpoint<OrderCreatedEvent>(
                        "send-order-to-history",
                        "order-history-service-group",
                        e => { e.ConfigureConsumer<OrderCreatedConsumer>(ctx); });

                    cfg.TopicEndpoint<SupplierCancelledEvent>(
                        "send-cancel-order-to-history",
                        "order-history-service-group",
                        e => { e.ConfigureConsumer<SupplierCancelledConsumer>(ctx); });

                    cfg.TopicEndpoint<DeliveryStartedEvent>(
                        "send-delivery-started-to-history",
                        "order-history-service-group",
                        e => { e.ConfigureConsumer<DeliveryStartedConsumer>(ctx); });

                    cfg.TopicEndpoint<DeliveryFinishedEvent>(
                        "send-delivery-finished-to-history",
                        "order-history-service-group",
                        e => { e.ConfigureConsumer<DeliveryFinishedConsumer>(ctx); });

                    cfg.TopicEndpoint<SupplierFinishedEvent>(
                        "send-supplier-finished-to-history",
                        "order-history-service-group",
                        e => { e.ConfigureConsumer<SupplierFinishedConsumer>(ctx); });
                });
            });
        });
    }

    public static void ConfigureApp(this IApplicationBuilder app)
    {
        // Run migrations
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}