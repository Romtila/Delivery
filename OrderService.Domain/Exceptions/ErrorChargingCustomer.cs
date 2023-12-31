using Delivery.BaseLib.Domain.Exceptions;

namespace OrderService.Domain.Exceptions;

public class ErrorChargingCustomer : BusinessLogicException
{
    public ErrorChargingCustomer()
        : base("It is not possible to process the order because the user does not have enough balance or does not exist.")
    {
    }
}