namespace Delivery.Entity.Entities;

public class Customer
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal Balance { get; set; }

    public List<Order>? Orders { get; set; }
    
    public List<Rating>? Ratings { get; set; }
}