namespace CustomerService.Application.Dtos.Responses;

public class CustomerResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}