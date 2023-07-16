using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Events.SupplierService;

namespace DeliveryService.Domain.Services.Interfaces;

public interface IDeliveryService
{
    DeliveryOrder Validate(long id);
    DeliveryOrder StartDelivery(long id);
    DeliveryOrder FinishDelivery(long id);
    Task HandleDelivery(SupplierFinishedEvent msg);
}