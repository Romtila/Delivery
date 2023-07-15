using Microsoft.EntityFrameworkCore;
using SupplierService.Domain.Entities;
using SupplierService.Infrastructure.Configurations;

namespace SupplierService.Infrastructure.Context;

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
        modelBuilder.ApplyConfiguration(new SupplierOrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierOrderItemEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<SupplierOrder> SupplierOrders { get; set; }
    public DbSet<SupplierOrderItem> SupplierOrderItems { get; set; }
}