namespace OrderService.Domain.Requests;

public class ChargeCustomerRequest
{
    public long CustomerId { get; set; }
    public decimal OrderPrice { get; set; }

    public static ChargeCustomerRequest Create(long customerId, decimal orderPrice)
    {
        return new ChargeCustomerRequest {CustomerId = customerId, OrderPrice = orderPrice};
    }
}