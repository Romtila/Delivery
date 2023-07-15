using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierService.Domain.Entities;

namespace SupplierService.Infrastructure.Configurations;

public class SupplierOrderItemEntityTypeConfiguration : IEntityTypeConfiguration<SupplierOrderItem>
{
    public void Configure(EntityTypeBuilder<SupplierOrderItem> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasDefaultValue(1)
            .IsRequired();
    }
}