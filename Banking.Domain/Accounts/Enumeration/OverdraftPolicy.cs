using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Domain.Accounts.Enumeration
{
    public class OverdraftPolicy
    {

        enum overdraftPolicy { NEVER, LIMITED };

        private int id;

        private OverdraftPolicy(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }
    }
}
