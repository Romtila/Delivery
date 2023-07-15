namespace SupplierService.Domain.Entities;

public class SupplierOrderItem
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}