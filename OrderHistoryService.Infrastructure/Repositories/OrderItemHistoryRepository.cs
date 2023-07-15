using Delivery.BaseLib.Infrastructure;
using OrderHistoryService.Domain.Entities;
using OrderHistoryService.Domain.Repositories;
using OrderHistoryService.Infrastructure.Context;

namespace OrderHistoryService.Infrastructure.Repositories;

public class OrderItemHistoryRepository : BaseRepository<OrderItemHistory>, IOrderItemHistoryRepository
{
    public OrderItemHistoryRepository(AppDbContext context) : base(context)
    {
    }
}