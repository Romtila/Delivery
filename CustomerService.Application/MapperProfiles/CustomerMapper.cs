using CustomerService.Application.Dtos.Requests;
using CustomerService.Application.Dtos.Responses;
using CustomerService.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace CustomerService.Application.MapperProfiles;

[Mapper]
public static partial class CustomerMapper
{
    public static partial CustomerResponse CustomerToCustomerResponse(this Customer customerResponse);
    public static partial Customer CustomerRequestToCustomer(this CustomerRequest customerRequest);
}