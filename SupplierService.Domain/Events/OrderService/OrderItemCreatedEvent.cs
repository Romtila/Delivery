namespace SupplierService.Domain.Events.OrderService;

public class OrderItemCreatedEvent
{
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
}