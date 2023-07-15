using Delivery.BaseLib.Domain.Exceptions;

namespace SupplierService.Domain.Exceptions;

public class SupplierOrderNotFoundException : NotFoundException
{
    public SupplierOrderNotFoundException() : base("Could not find an order with the given id")
    {
    }
}