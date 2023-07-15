namespace DeliveryService.Domain.Events.SupplierService;

public class SupplierFinishedEvent
{
    public long OrderId { get; set; }
    public long CustomerId { get; set; }
}