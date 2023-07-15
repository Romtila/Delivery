using Delivery.BaseLib.Infrastructure;
using SupplierService.Domain.Entities;
using SupplierService.Domain.Repositories;
using SupplierService.Infrastructure.Context;

namespace SupplierService.Infrastructure.Repositories;

public class SupplierRepository : BaseRepository<SupplierOrder>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context)
    {
    }
}