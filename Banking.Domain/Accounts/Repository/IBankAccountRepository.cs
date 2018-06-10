using Banking.Domain.Accounts.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Domain.Accounts.Repository
{
    public interface IBankAccountRepository 
    {
        BankAccount FindByNumber(string accountNumber);
        BankAccount FindByNumberLocked(string accountNumber);
        bool AccountEnabled(string accountNumber);
        List<BankAccount> GetByCustomerId(int customerId);
        List<BankAccount> GetByUsername(string username);
        BankAccount FindById(int bankAccountId);
        bool InsufficientBalance(string accountNumber, decimal amount);
        bool OwnAccount(string username, string accountNumber);

        void Update(BankAccount accountNumber);

    }
}
