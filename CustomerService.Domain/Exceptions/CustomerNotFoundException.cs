using Delivery.BaseLib.Domain.Exceptions;

namespace CustomerService.Domain.Exceptions;

public class CustomerNotFoundException : NotFoundException
{
    public CustomerNotFoundException() : base("Could not find customer with the given id")
    {
    }
}