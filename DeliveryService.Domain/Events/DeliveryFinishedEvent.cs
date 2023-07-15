namespace DeliveryService.Domain.Events;

public class DeliveryFinishedEvent
{
    public long OrderId { get; set; }
}