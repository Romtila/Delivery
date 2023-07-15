using Microsoft.EntityFrameworkCore;
using OrderHistoryService.Domain.Entities;
using OrderHistoryService.Infrastructure.Configurations;

namespace OrderHistoryService.Infrastructure.Context;

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
        modelBuilder.ApplyConfiguration(new OrderItemHistoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderHistoryEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<OrderHistory> OrderHistories { get; set; }
    public DbSet<OrderItemHistory> OrderItemHistories { get; set; }
}