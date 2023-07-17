using System.Reflection;
using Delivery.BaseLib.Core.Filters;
using Delivery.BaseLib.Infrastructure.Transactions;
using DeliveryService.Application.Consumers;
using DeliveryService.Application.Services;
using DeliveryService.Domain.Events.SupplierService;
using DeliveryService.Domain.Services.Interfaces;
using DeliveryService.Infrastructure.Context;
using DeliveryService.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.API;

public static class DiExtension
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(config => { config.Filters.Add<ExceptionFilter>(); });

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Delivery.DeliveryService.Infrastructure")));

        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        services.Scan(scan => scan.FromAssemblyOf<DeliveryAppService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<SupplierRepository>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<Domain.Services.DeliveryService>()
            .AddClasses(classes => classes.InNamespaces("*.Services"))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddHttpClient<IDeliveryService, Domain.Services.DeliveryService>(client =>
        {
            client.BaseAddress = new Uri(configuration["CustomerService"] ?? string.Empty);
        });

        services.AddMassTransit(bus =>
        {
            bus.AddRider(rider =>
            {
                rider.AddConsumer<SupplierFinishedConsumer>();

                rider.UsingKafka((ctx, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("Kafka"));
                    cfg.TopicEndpoint<SupplierFinishedEvent>(
                        "send-orders-to-delivery",
                        "delivery-service-group",
                        e => { e.ConfigureConsumer<SupplierFinishedConsumer>(ctx); });
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