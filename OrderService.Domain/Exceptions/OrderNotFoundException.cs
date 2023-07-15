using Delivery.BaseLib.Domain.Exceptions;

namespace OrderService.Domain.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException() : base("Could not find an order with the given id")
    {
    }
}