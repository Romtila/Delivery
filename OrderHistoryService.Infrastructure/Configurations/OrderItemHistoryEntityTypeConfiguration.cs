using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderHistoryService.Domain.Entities;

namespace OrderHistoryService.Infrastructure.Configurations;

public class OrderItemHistoryEntityTypeConfiguration : IEntityTypeConfiguration<OrderItemHistory>
{
    public void Configure(EntityTypeBuilder<OrderItemHistory> builder)
    {
        builder.Property(x => x.OrderItemId)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();
    }
}