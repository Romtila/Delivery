using CustomerService.Application.Dtos.Requests;
using CustomerService.Application.Dtos.Responses;

namespace CustomerService.Application.Services.Interfaces;

public interface ICustomerAppService
{
    CustomerResponse GetById(long id);
    CustomerResponse Add(CustomerRequest request);
    CustomerResponse AddBalance(AddBalanceRequest request);
    CustomerResponse Update(long id, CustomerRequest request);
    void Remove(long id);
    void ChargeCustomer(ChargeCustomerRequest request);
}