using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Domain.Customers.Enumeration
{
    public class CustomerStatus
    {
        enum status { Inactive, Active };

        private int id;

        private CustomerStatus(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        //public static String convertIntToString(int status)
        //{
        //    for (CustomerStatus customerStatus : CustomerStatus.values())
        //    {
        //        if (customerStatus.getId() == status)
        //        {
        //            return customerStatus.toString();
        //        }
        //    }
        //    return "";
        //}
    }
}
