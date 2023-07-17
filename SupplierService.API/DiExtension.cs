using System.Reflection;
using Delivery.BaseLib.Core.Filters;
using Delivery.BaseLib.Infrastructure.Transactions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SupplierService.Application.Consumers;
using SupplierService.Application.Services;
using SupplierService.Domain.Events.OrderService;
using SupplierService.Infrastructure.Context;
using SupplierService.Infrastructure.Repositories;

namespace SupplierService.API;

public static class DiExtension
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(config => { config.Filters.Add<ExceptionFilter>(); });

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Delivery.SupplierService.Infra")));

        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        services.Scan(scan => scan.FromAssemblyOf<SupplierAppService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<SupplierRepository>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<Domain.Services.SupplierService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddMassTransit(bus =>
        {
            bus.AddConsumer<OrderCreatedConsumer>();

            bus.AddRider(rider =>
            {
                rider.UsingKafka((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("Kafka"));

                    cfg.TopicEndpoint<OrderCreatedEvent>(
                        "send-orders-to-supplier",
                        "supplier-service-group",
                        e => { e.ConfigureConsumer<OrderCreatedConsumer>(ctx); });
                });
            });
        });
    }

    public static void ConfigureApp(this IApplicationBuilder app)
    {
        // Run migrations
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        if (serviceScope is null) return;

        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}