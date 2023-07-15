using DeliveryService.Domain.Entities.Enums;

namespace DeliveryService.Domain.Entities;

public class DeliveryOrder
{
    public long Id { get; set; }
    public int CustomerId { get; set; }
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string DeliveryAddress { get; set; } = string.Empty;
    public DeliveryOrderStatus Status { get; set; }
}