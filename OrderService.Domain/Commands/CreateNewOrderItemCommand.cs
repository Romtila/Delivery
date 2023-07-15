namespace OrderService.Domain.Commands;

public class CreateNewOrderItemCommand
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}