using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderHistoryService.Domain.Entities;

namespace OrderHistoryService.Infrastructure.Configurations;

public class OrderHistoryEntityTypeConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.Property(x => x.OrderId)
            .IsRequired();

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Navigation(x => x.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .AutoInclude();
    }
}