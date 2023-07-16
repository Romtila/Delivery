using CustomerService.Application.Dtos.Requests;
using CustomerService.Application.Dtos.Responses;
using CustomerService.Application.MapperProfiles;
using CustomerService.Application.Services.Interfaces;
using CustomerService.Domain.Repositories;
using CustomerService.Domain.Services.Interfaces;
using Delivery.BaseLib.Infrastructure.Transactions;

namespace CustomerService.Application.Services;

public class CustomerAppService : ICustomerAppService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerService _customerService;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerAppService(ICustomerService customerService, ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerService = customerService;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public CustomerResponse GetById(long id)
    {
        var customer = _customerService.Validate(id);

        return customer.CustomerToCustomerResponse();
    }

    public CustomerResponse Add(CustomerRequest request)
    {
        var customer = request.CustomerRequestToCustomer();

        _customerRepository.Add(customer);
        _unitOfWork.Commit();

        return customer.CustomerToCustomerResponse();
    }

    public CustomerResponse AddBalance(AddBalanceRequest request)
    {
        var customer = _customerService.Validate(request.Id);

        _customerService.AddBalance(customer, request.Balance);
        _unitOfWork.Commit();

        return customer.CustomerToCustomerResponse();
    }

    public CustomerResponse Update(long id, CustomerRequest request)
    {
        var customer = _customerService.Validate(id);

        customer.Name = request.Name;
        customer.Address = request.Address;

        _customerRepository.Update(customer);
        _unitOfWork.Commit();

        return customer.CustomerToCustomerResponse();
    }

    public void Remove(long id)
    {
        _customerService.Remove(id);
        _unitOfWork.Commit();
    }

    public void ChargeCustomer(ChargeCustomerRequest request)
    {
        _customerService.ChargeCustomer(request.CustomerId, request.OrderPrice);
        _unitOfWork.Commit();
    }
}