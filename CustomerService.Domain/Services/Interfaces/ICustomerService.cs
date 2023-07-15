using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Services.Interfaces;

public interface ICustomerService
{
    Task<Customer> Validate(long id, CancellationToken ct);
    Task Remove(long id, CancellationToken ct);
    Task AddBalance(Customer customer, decimal balance, CancellationToken ct);
    Task ChargeCustomer(long customerId, decimal orderTotal, CancellationToken ct);
}