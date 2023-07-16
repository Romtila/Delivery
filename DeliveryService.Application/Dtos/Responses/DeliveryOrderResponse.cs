using Delivery.BaseLib.Common.Enums;

namespace DeliveryService.Application.Dtos.Responses;

public class DeliveryOrderResponse
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public long OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public EnumValue Status { get; set; } = new();
}