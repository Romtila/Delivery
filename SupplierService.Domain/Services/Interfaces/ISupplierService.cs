using SupplierService.Domain.Entities;

namespace SupplierService.Domain.Services.Interfaces;

public interface ISupplierService
{
    SupplierOrder Validate(long id);
    SupplierOrder FinishOrder(long id);
    SupplierOrder CancelOrder(long id);
    void HandleNewOrder(SupplierOrder entity);
}