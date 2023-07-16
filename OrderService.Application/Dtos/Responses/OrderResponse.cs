namespace OrderService.Application.Dtos.Responses;

public class OrderResponse
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public List<OrderItemResponse> Items { get; set; } = new();
    public decimal Total { get; set; }
}