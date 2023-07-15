namespace OrderService.Domain.Entities;

public class Order
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal Total { get; set; }
}