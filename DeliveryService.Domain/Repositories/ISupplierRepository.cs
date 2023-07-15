using Delivery.BaseLib.Domain.Repositories;
using DeliveryService.Domain.Entities;

namespace DeliveryService.Domain.Repositories;

public interface ISupplierRepository : IRepositoryBase<DeliveryOrder>
{
}