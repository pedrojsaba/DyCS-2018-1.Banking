using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Infrastructure.Common
{
    public class Functions
    {
        public static string GetConnectionString()
        {
            return "Data Source=HP\\SQLEXPRESS;Initial Catalog=Banking;User ID=sa;Password=shaffer";
        }
    }
}
