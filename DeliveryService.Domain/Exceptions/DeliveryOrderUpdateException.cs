using Delivery.BaseLib.Domain.Exceptions;

namespace DeliveryService.Domain.Exceptions;

public class DeliveryOrderUpdateException : BusinessLogicException
{
    public DeliveryOrderUpdateException(string description)
        : base($"It is not possible update the order status. Reason: Order status currently is {description}.")
    {
    }
}