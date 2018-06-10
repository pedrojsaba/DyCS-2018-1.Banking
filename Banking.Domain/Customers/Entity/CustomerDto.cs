using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Domain.Customers.Entity
{
    public class CustomerDto
    {
        public Int64 id { get; set; }
        public String name { get; set; }
        public String status { get; set; }

        public CustomerDto()
        {
        }

        public CustomerDto(Int64 id, String name, String status)
        {
            this.id = id;
            this.name = name;
            this.status = status;
        }

    }
}
