using Banking.Domain.Accounts.Abstraction;
using Banking.Domain.Accounts.Enumeration;
using Banking.Domain.Customers.Entity;
using System;

namespace Banking.Domain.Accounts.Entity
{
    public class BankAccount
    {
        private long id;
        private string number;
        public decimal? Balance { get; set; }
        private OverdraftPolicy overdraftPolicy;
        private BankAccountState state;
        private Customer customer;

        public BankAccount()
        {
        }

        public BankAccount(long id, String number, Decimal balance, OverdraftPolicy overdraftPolicy, BankAccountState state)
        {
            this.id = id;
            this.number = number;
            this.Balance = balance;
            this.overdraftPolicy = overdraftPolicy;
            this.state = state;
        }

        public void withdrawMoney(Decimal amount)
        {
            this.state.withdrawMoney(amount);
        }

        public void depositMoney(Decimal amount)
        {
            this.state.depositMoney(amount);
        }

        public bool hasIdentity()
        {
            return this.id > 0 && !this.number.Trim().Equals("");
        }

        public long getId()
        {
            return id;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public String getNumber()
        {
            return number;
        }

        public void setNumber(String number)
        {
            this.number = number;
        }

        

        public OverdraftPolicy getOverdraftPolicy()
        {
            return overdraftPolicy;
        }

        public void setOverdraftPolicy(OverdraftPolicy overdraftPolicy)
        {
            this.overdraftPolicy = overdraftPolicy;
        }

        public BankAccountState getState()
        {
            return state;
        }

        public void setState(BankAccountState state)
        {
            this.state = state;
        }

        public Customer getCustomer()
        {
            return customer;
        }

        public void setCustomer(Customer customer)
        {
            this.customer = customer;
        }
    }
}
