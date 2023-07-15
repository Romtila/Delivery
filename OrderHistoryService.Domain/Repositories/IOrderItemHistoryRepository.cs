using Delivery.BaseLib.Domain.Repositories;
using OrderHistoryService.Domain.Entities;

namespace OrderHistoryService.Domain.Repositories;

public interface IOrderItemHistoryRepository : IRepositoryBase<OrderItemHistory>
{
}