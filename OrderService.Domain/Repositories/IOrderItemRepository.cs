using Delivery.BaseLib.Domain.Repositories;
using OrderService.Domain.Entities;

namespace OrderService.Domain.Repositories;

public interface IOrderItemRepository : IRepositoryBase<OrderItem>
{
}