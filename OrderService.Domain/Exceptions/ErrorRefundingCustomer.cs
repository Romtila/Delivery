using Delivery.BaseLib.Domain.Exceptions;

namespace OrderService.Domain.Exceptions;

public class ErrorRefundingCustomer : BusinessLogicException
{
    public ErrorRefundingCustomer() : base("It is not possible to cancel the order because the refund process failed.")
    {
    }
}