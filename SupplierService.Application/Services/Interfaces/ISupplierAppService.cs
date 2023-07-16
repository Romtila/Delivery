using SupplierService.Application.Dtos.Responses;

namespace SupplierService.Application.Services.Interfaces;

public interface ISupplierAppService
{
    SupplierOrderResponse Get(long id);
    IEnumerable<SupplierOrderResponse> Get();
    Task FinishOrder(long id);
    Task CancelOrder(long id);
}