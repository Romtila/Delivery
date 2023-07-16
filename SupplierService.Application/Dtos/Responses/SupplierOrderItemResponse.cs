namespace SupplierService.Application.Dtos.Responses;

public class SupplierOrderItemResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}