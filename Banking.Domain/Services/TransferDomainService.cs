using Banking.Domain.Accounts.Entity;
using Banking.Domain.Common.ValueObject;
using Banking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Domain.Services
{
    public class TransferDomainService
    {

        public void PerformTransfer(BankAccount originAccount, BankAccount destinationAccount, Decimal amount)
        {
            ValidateData(originAccount, destinationAccount, amount);
            originAccount.withdrawMoney(amount);
            destinationAccount.depositMoney(amount);
        }

        private static void ValidateData(BankAccount originAccount, BankAccount destinationAccount, Decimal amount)
        {
            if (originAccount == null || destinationAccount == null)
            {
                throw new InvalidTransferBankAccountException();
            }
            if (originAccount.getNumber().Equals(destinationAccount.getNumber()))
            {
                throw new SameTransferBankAccountException();
            }
        }



    }
}
