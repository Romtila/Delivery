namespace Delivery.Entity.Entities;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public List<Order>? Orders { get; set; }

    public List<Rating>? Ratings { get; set; }

    public List<MenuItem>? MenuItems { get; set; }
}