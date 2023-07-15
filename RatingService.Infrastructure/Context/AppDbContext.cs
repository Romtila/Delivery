using Microsoft.EntityFrameworkCore;

namespace RatingService.Infrastructure.Context;

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
        //modelBuilder.ApplyConfiguration(new());

        base.OnModelCreating(modelBuilder);
    }
}