using System.Reflection;
using Delivery.BaseLib.Core.Filters;
using Delivery.BaseLib.Infrastructure.Transactions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Consumers;
using OrderService.Application.Services;
using OrderService.Domain.Events.SupplierService;
using OrderService.Domain.Services.Interfaces;
using OrderService.Infrastructure.Context;
using OrderService.Infrastructure.Repositories;

namespace OrderService.API;

public static class DiExtension
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(config => { config.Filters.Add<ExceptionFilter>(); });

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Delivery.OrderService.Infra")));

        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        services.Scan(scan => scan.FromAssemblyOf<OrderAppService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<OrderRepository>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<Domain.Services.OrderService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddHttpClient<IOrderService, Domain.Services.OrderService>(client => { client.BaseAddress = new Uri(configuration["CustomerService"]); });

        services.AddMassTransit(bus =>
        {
            bus.AddRider(rider =>
            {
                rider.UsingKafka((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("Kafka"));

                    cfg.TopicEndpoint<SupplierCancelledEvent>(
                        "supplier-cancelled",
                        "order-service-group",
                        e => { e.ConfigureConsumer<SupplierCancelledConsumer>(ctx); });
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