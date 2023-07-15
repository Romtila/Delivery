namespace OrderHistoryService.Domain.Events.OrderService;

public class OrderItemCreatedEvent
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}