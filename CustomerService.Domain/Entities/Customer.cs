namespace CustomerService.Domain.Entities;

public class Customer
{
    public long Id { get; set; }
    public bool IsActive { get; set; } = true;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0.0m;
}