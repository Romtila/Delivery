using DeliveryService.Application.Dtos.Responses;

namespace DeliveryService.Application.Services.Interfaces;

public interface IDeliveryAppService
{
    DeliveryOrderResponse Get(long id);
    IEnumerable<DeliveryOrderResponse> Get();
    Task StartDelivery(long id);
    Task FinishDelivery(long id);
}