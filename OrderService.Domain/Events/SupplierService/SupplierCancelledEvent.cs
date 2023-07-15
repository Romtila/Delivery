namespace OrderService.Domain.Events.SupplierService;

public class SupplierCancelledEvent
{
    public long OrderId { get; set; }
}