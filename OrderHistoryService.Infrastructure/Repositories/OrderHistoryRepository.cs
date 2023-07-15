using Delivery.BaseLib.Infrastructure;
using OrderHistoryService.Domain.Entities;
using OrderHistoryService.Domain.Repositories;
using OrderHistoryService.Infrastructure.Context;

namespace OrderHistoryService.Infrastructure.Repositories;

public class OrderHistoryRepository : BaseRepository<OrderHistory>, IOrderHistoryRepository
{
    public OrderHistoryRepository(AppDbContext context) : base(context)
    {
    }
}