using Delivery.BaseLib.Infrastructure;
using SupplierService.Domain.Entities;
using SupplierService.Domain.Repositories;
using SupplierService.Infrastructure.Context;

namespace SupplierService.Infrastructure.Repositories;

public class SupplierOrderItemRepository : BaseRepository<SupplierOrderItem>, ISupplierOrderItemRepository
{
    public SupplierOrderItemRepository(AppDbContext context) : base(context)
    {
    }
}