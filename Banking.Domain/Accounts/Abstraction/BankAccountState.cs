using Banking.Common.Notification;
using Banking.Domain.Accounts.Entity;
using Banking.Domain.Accounts.Enumeration;
using Banking.Domain.Common.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Domain.Accounts.Abstraction
{
    public abstract class BankAccountState
    {
        protected BankAccountStateEnum bankAccountStateEnum;
        protected BankAccount bankAccount;

        public BankAccountState()
        {
        }

        public BankAccountState(BankAccount bankAccount)
        {
            this.bankAccount = bankAccount;
        }

        protected abstract void Lock();

        protected abstract void unlock();

        protected abstract void freeze();

        protected abstract void close();

        protected abstract BankAccountState transitionState();

        public void withdrawMoney(Decimal amount)
        {
            bankAccount.Balance = bankAccount.Balance - amount;
        }

        public void depositMoney(Decimal amount)
        {
            bankAccount.Balance = bankAccount.Balance + amount;
        }

        protected Notification withdrawValidation(Decimal amount)
        {
            Notification notification = new Notification();
            this.validateAmount(notification, amount);
            this.validateBankAccount(notification);
            this.validateBalance(notification);
            this.validateOverdraftPolicy(notification, amount);
            return notification;
        }

        protected Notification depositValidation(Decimal amount)
        {
            Notification notification = new Notification();
            this.validateAmount(notification, amount);
            this.validateBankAccount(notification);
            return notification;
        }

        private void validateAmount(Notification notification, Decimal amount)
        {
            //if (amount == null)
            //{
            //    notification.addError("amount is missing");
            //    return;
            //}
            //if (amount.amount().signum() <= 0)
            //{
            //    notification.addError("The amount must be greater than zero");
            //}
        }

        private void validateBankAccount(Notification notification)
        {
            if (this.bankAccount == null)
            {
                notification.addError("account is missing");
                return;
            }
            if (!this.bankAccount.hasIdentity())
            {
                notification.addError("account has no identity");
            }
        }

        private void validateBalance(Notification notification)
        {
            if (this.bankAccount == null)
            {
                return;
            }
            if (bankAccount.Balance == null)
            {
                notification.addError("balance cannot be null");//TODO: Cambiar mensaje
            }
        }

        private void validateOverdraftPolicy(Notification notification, Decimal amount)
        {
            //if (this.bankAccount == null)
            //{
            //    return;
            //}
            //if (this.bankAccount.getOverdraftPolicy() == null)
            //{
            //    notification.addError("overdraftPolicy is missing");
            //    return;
            //}
            //Notification overdrafNotification = this.bankAccount.getOverdraftPolicy().check(this.bankAccount, amount);
            //if (!overdrafNotification.hasErrors())
            //{
            //    return;
            //}
            //foreach (Error error in  overdrafNotification.getErrors())
            //{
            //    notification.addError(error.getMessage());
            //}
        }

        public BankAccount getBankAccount()
        {
            return bankAccount;
        }

        public void setBankAccount(BankAccount bankAccount)
        {
            this.bankAccount = bankAccount;
        }
    }
}
