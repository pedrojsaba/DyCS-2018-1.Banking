using System;
using Banking.Application.Customers.Service;
using Banking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {

        private readonly CustomerApplication customerApplication;

        public CustomerController()
        {
            this.customerApplication = new CustomerApplication(new CustomerEfRepository());
        }

        [HttpGet]
        [Route("Customers")]
        public Object GetCustomers(int skip, int pageSize)
        {
            return customerApplication.getCustomersDto(skip, pageSize);
        }

    }
}