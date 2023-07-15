using Delivery.BaseLib.Infrastructure;
using DeliveryService.Domain.Entities;
using DeliveryService.Domain.Repositories;
using DeliveryService.Infrastructure.Context;

namespace DeliveryService.Infrastructure.Repositories;

public class SupplierRepository : BaseRepository<DeliveryOrder>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context)
    {
    }
}