using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Application.Customers.Service;
using Banking.Infrastructure.Customers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Produces("application/json")]
    [EnableCors("AllowCors"), Route("api/Customer")]
    public class CustomerController : Controller
    {

        private readonly CustomerApplication customerApplication;

        public CustomerController()
        {
            this.customerApplication = new CustomerApplication(new CustomerAdoNetRepository());
        }

        [HttpGet]
        [Route("Customers")]
        public Object GetCustomers(int skip, int pageSize)
        {
            return customerApplication.getCustomersDto(skip, pageSize);
        }

    }
}