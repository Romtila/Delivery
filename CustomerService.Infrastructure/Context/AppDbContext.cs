using CustomerService.Domain.Entities;
using CustomerService.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Context;

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
        modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Customer> Customers { get; set; }
}