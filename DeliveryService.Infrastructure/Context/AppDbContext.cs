using DeliveryService.Domain.Entities;
using DeliveryService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DeliveryOrderEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
}