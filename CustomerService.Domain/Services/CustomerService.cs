using CustomerService.Domain.Entities;
using CustomerService.Domain.Exceptions;
using CustomerService.Domain.Repositories;
using CustomerService.Domain.Services.Interfaces;
using Delivery.BaseLib.Domain.Exceptions;

namespace CustomerService.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer> Validate(long id, CancellationToken ct)
    {
        var customer = await _repository.FindAsync(id, ct);

        if (customer is null || !customer.IsActive)
            throw new CustomerNotFoundException();

        return customer;
    }

    public async Task Remove(long id, CancellationToken ct)
    {
        var customer = await Validate(id, ct);

        if (customer.Balance > 0M)
            throw new BusinessLogicException("It is not possible to delete a customer that has remaining balance");

        customer.IsActive = false;
        await _repository.UpdateAsync(customer, ct);
    }

    public async Task AddBalance(Customer customer, decimal balance, CancellationToken ct)
    {
        customer.Balance += balance;
        await _repository.UpdateAsync(customer, ct);
    }

    public async Task ChargeCustomer(long customerId, decimal orderTotal, CancellationToken ct)
    {
        var customer = await Validate(customerId, ct);

        if (customer.Balance < orderTotal)
            throw new BusinessLogicException("The customer does not have enough balance to pay for this order.");

        customer.Balance -= orderTotal;
        await _repository.UpdateAsync(customer, ct);
    }
}