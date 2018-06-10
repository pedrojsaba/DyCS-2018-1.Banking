using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Application.Customers.Service;
using Banking.Infrastructure.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly CustomerApplication customerApplication;

        public ValuesController()
        {
            this.customerApplication = new CustomerApplication(new CustomerAdoNetRepository());
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("albums")]
        public Object GetAlbums(int skip, int pageSize)
        {
            return customerApplication.getCustomersDto(skip, pageSize);
        }

    }
}
