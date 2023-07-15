using System.Text.Json.Serialization;

namespace OrderService.Domain.Requests;

public class RefundCustomerRequest
{
    [JsonPropertyName("id")] public long CustomerId { get; set; }
    [JsonPropertyName("balance")] public decimal TotalPrice { get; set; }


    public static RefundCustomerRequest Create(long customerId, decimal totalPrice)
    {
        return new RefundCustomerRequest {CustomerId = customerId, TotalPrice = totalPrice};
    }
}