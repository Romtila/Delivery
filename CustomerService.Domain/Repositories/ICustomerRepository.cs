using CustomerService.Domain.Entities;
using Delivery.BaseLib.Domain.Repositories;

namespace CustomerService.Domain.Repositories;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
}