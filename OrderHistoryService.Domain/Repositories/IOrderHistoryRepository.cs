using Delivery.BaseLib.Domain.Repositories;
using OrderHistoryService.Domain.Entities;

namespace OrderHistoryService.Domain.Repositories;

public interface IOrderHistoryRepository : IBaseRepository<OrderHistory>
{
}