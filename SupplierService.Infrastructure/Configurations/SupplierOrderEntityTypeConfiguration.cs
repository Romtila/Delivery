using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupplierService.Domain.Entities;
using SupplierService.Domain.Entities.Enums;

namespace SupplierService.Infrastructure.Configurations;

public class SupplierOrderEntityTypeConfiguration : IEntityTypeConfiguration<SupplierOrder>
{
    public void Configure(EntityTypeBuilder<SupplierOrder> builder)
    {
        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.OrderId)
            .IsRequired();

        builder.Navigation(x => x.Items)
            .AutoInclude();

        builder.Property(x => x.Status)
            .HasDefaultValue(SupplierOrderStatus.Preparing)
            .IsRequired();
    }
}