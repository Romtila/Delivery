using CustomerService.Application.Services;
using CustomerService.Infrastructure.Context;
using CustomerService.Infrastructure.Repositories;
using Delivery.BaseLib.Core.Filters;
using Delivery.BaseLib.Infrastructure.Transactions;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.API;

public static class DiExtension
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(config => { config.Filters.Add<ExceptionFilter>(); });

        services.AddDbContext<AppDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("Delivery.CustomerService.Infrastructure")));
        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        services.Scan(scan => scan.FromAssemblyOf<CustomerAppService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<CustomerRepository>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblyOf<Domain.Services.CustomerService>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());
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