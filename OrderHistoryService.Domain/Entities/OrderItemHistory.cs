namespace OrderHistoryService.Domain.Entities;

public class OrderItemHistory
{
    public long Id { get; set; }
    public long OrderItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}