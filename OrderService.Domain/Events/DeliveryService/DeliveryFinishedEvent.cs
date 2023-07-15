namespace OrderService.Domain.Events.DeliveryService;

public class DeliveryFinishedEvent
{
    public long OrderId { get; set; }
}