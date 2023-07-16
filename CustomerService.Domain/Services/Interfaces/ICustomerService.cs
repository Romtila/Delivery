using CustomerService.Domain.Entities;

namespace CustomerService.Domain.Services.Interfaces;

public interface ICustomerService
{
    Customer Validate(long id);
    void Remove(long id);
    void AddBalance(Customer customer, decimal balance);
    void ChargeCustomer(long customerId, decimal orderTotal);
}