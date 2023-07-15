namespace OrderService.Domain.Events;

public class OrderCreatedEvent
{
    public long OrderId { get; set; }
    public long CustomerId { get; set; }
    public decimal Total { get; set; }
    public IEnumerable<OrderItemCreatedEvent> Items { get; set; } = new List<OrderItemCreatedEvent>();
}