namespace OrderHistoryService.Domain.Events.SupplierService;

public class SupplierCancelledEvent
{
    public long OrderId { get; set; }
}