using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Domain.Accounts.Enumeration
{
    public class BankAccountStateEnum
    {
        enum bankAccountStateEnum { Active, Closed, Frozen, Locked, Overdrawn };

        private int id;

        private BankAccountStateEnum(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        //public static String convertIntToString(int state)
        //{
        //    for (BankAccountStateEnum bankAccountState : BankAccountStateEnum.values())
        //    {
        //        if (bankAccountState.getId() == state)
        //        {
        //            return bankAccountState.toString();
        //        }
        //    }
        //    return "";
        //}
    }
}
