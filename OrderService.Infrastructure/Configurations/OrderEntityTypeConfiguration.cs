using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Configurations;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Navigation(x => x.Items)
            .AutoInclude();

        builder.Property(x => x.Total)
            .HasDefaultValue(0M);
    }
}