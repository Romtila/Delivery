namespace SupplierService.Domain.Events;

public class SupplierFinishedEvent
{
    public long OrderId { get; set; }
    public long CustomerId { get; set; }
}