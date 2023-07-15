using Delivery.BaseLib.Domain.Repositories;
using SupplierService.Domain.Entities;

namespace SupplierService.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<SupplierOrder>
{
}