using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Events.SupplierService;

namespace DeliveryService.Domain.Services;

public interface IDeliveryService
{
    Task<DeliveryOrder> Validate(long id, CancellationToken ct);
    Task<DeliveryOrder> StartDelivery(long id, CancellationToken ct);
    Task<DeliveryOrder> FinishDelivery(long id, CancellationToken ct);
    Task HandleDelivery(SupplierFinishedEvent msg, CancellationToken ct);
}