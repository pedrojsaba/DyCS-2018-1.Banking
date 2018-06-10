using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain.Common.ValueObject
{
    public static class MoneyExtenshions
    {
        public static string MoneyToText(this decimal amount, Currency currency)
        {
            return new Money(amount, currency).Text;
        }

        public static string MoneyToText(this double amount, Currency currency)
        {
            return new Money(amount, currency).Text;
        }

        public static string MoneyToText(this int amount, Currency currency)
        {
            return new Money(amount, currency).Text;
        }

        public static Money ToMoney(this decimal amount, Currency currency)
        {
            return new Money(amount, currency);
        }

        public static Money ToMoney(this double amount, Currency currency)
        {
            return new Money(amount, currency);
        }

        public static Money ToMoney(this int amount, Currency currency)
        {
            return new Money(amount, currency);
        }

    }
}
