using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OrderService.Infrastructure.Context;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbContextBuilder = new DbContextOptionsBuilder();

        var sensitiveLogging = false;

        try
        {
            sensitiveLogging = bool.Parse(configuration
                .GetSection("Logging")
                .GetSection("EntityFrameworkCore")["EnableSensitiveDataLogging"] ?? string.Empty);
        }
        catch
        {
            // ignored
        }

        dbContextBuilder.EnableSensitiveDataLogging(sensitiveLogging);

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        dbContextBuilder.UseNpgsql(connectionString);

        return new AppDbContext(dbContextBuilder.Options);
    }
}