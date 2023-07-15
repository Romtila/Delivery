using Delivery.BaseLib.Domain.Repositories;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Repositories;

public interface IOrderRepository : IRepositoryBase<Order>
{
}