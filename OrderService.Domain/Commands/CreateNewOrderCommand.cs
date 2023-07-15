namespace OrderService.Domain.Commands;

public class CreateNewOrderCommand
{
    public long CustomerId { get; set; }
    public IEnumerable<CreateNewOrderItemCommand> Items { get; set; } = new List<CreateNewOrderItemCommand>();
}