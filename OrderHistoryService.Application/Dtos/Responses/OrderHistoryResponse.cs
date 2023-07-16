using Delivery.BaseLib.Common.Enums;

namespace OrderHistoryService.Application.Dtos.Responses;

public class OrderHistoryResponse
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long CustomerId { get; set; }
    public IEnumerable<OrderItemHistoryResponse> Items { get; set; } = new List<OrderItemHistoryResponse>();
    public decimal Total { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public EnumValue Status { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}