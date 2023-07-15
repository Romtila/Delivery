using Delivery.BaseLib.Domain.Exceptions;

namespace OrderHistoryService.Domain.Exceptions;

public class OrderHistoryNotFoundException : NotFoundException
{
    public OrderHistoryNotFoundException() : base("Could not find an order history with the given id")
    {
    }
}