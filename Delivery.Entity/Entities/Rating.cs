namespace Delivery.Entity.Entities;

public class Rating
{
    public long Id { get; set; }
    public double Value { get; set; }

    public long UserId { get; set; }
    public Customer Customer { get; set; } = new();

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = new();
}