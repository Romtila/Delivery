namespace OrderHistoryService.Domain.Events.OrderService;

public class OrderCreatedEvent
{
    public long OrderId { get; set; }
    public long CustomerId { get; set; }
    public IEnumerable<OrderItemCreatedEvent> Items { get; set; } = new List<OrderItemCreatedEvent>();
    public decimal Total { get; set; }
}