using OrderHistoryService.Domain.Entities.Enums;

namespace OrderHistoryService.Domain.Entities;

public class OrderHistory
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long CustomerId { get; set; }
    public IEnumerable<OrderItemHistory> Items { get; set; } = new List<OrderItemHistory>();
    public decimal Total { get; set; }
    public string? DeliveryAddress { get; set; }
    public OrderHistoryStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}