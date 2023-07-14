namespace Delivery.Entity.Entities;

public class Payment
{
    public long Id { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;

    public long OrderId { get; set; }
    public Order Order { get; set; } = new();
}