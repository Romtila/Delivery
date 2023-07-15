using SupplierService.Domain.Entities.Enums;

namespace SupplierService.Domain.Entities;

public class SupplierOrder
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public long OrderId { get; set; }
    public IEnumerable<SupplierOrderItem> Items { get; set; } = new List<SupplierOrderItem>();
    public SupplierOrderStatus Status { get; set; }
}