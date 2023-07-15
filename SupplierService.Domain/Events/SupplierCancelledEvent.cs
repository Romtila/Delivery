namespace SupplierService.Domain.Events;

public class SupplierCancelledEvent
{
    public long OrderId { get; set; }
}