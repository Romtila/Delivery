namespace DeliveryService.Domain.Events;

public class DeliveryStartedEvent
{
    public long OrderId { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
}