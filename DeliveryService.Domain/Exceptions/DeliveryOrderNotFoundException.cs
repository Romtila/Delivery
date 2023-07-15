using Delivery.BaseLib.Domain.Exceptions;

namespace DeliveryService.Domain.Exceptions;

public class DeliveryOrderNotFoundException : NotFoundException
{
    public DeliveryOrderNotFoundException() : base("Could not find an order with the given id")
    {
    }
}