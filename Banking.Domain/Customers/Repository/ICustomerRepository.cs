using Banking.Domain.Customers.Entity;
using System.Collections.Generic;

namespace Banking.Domain.Customers.Repository
{
    public interface ICustomerRepository
    {
         List<CustomerDto> getCustomersDto(int skip, int pageSize);
    }
}
