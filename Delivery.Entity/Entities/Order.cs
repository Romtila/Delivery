namespace Delivery.Entity.Entities;

public class Order
{
    public long Id { get; set; }
    public decimal OrderTotal { get; set; }
    public string DeliveryStatus { get; set; } = string.Empty;

    public long UserId { get; set; }
    public Customer Customer { get; set; } = new();

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = new();

    public long? PaymentId { get; set; }
    public Payment? Payment { get; set; }
}