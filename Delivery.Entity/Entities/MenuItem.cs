namespace Delivery.Entity.Entities;

public class MenuItem
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = new();
}