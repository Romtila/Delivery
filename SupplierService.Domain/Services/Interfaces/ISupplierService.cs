using SupplierService.Domain.Entities;

namespace SupplierService.Domain.Services.Interfaces;

public interface ISupplierService
{
    Task<SupplierOrder> Validate(long id, CancellationToken ct);
    Task<SupplierOrder> FinishOrder(long id, CancellationToken ct);
    Task<SupplierOrder> CancelOrder(long id, CancellationToken ct);
    Task HandleNewOrder(SupplierOrder entity, CancellationToken ct);
}