namespace OrderService.Domain.Events.DeliveryService;

public class DeliveryStartedEvent
{
    public long OrderId { get; set; }
}