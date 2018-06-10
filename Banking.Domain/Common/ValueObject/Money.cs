/**********************************************************
 * This class represents amount of money in selected currency,
 * which allows to avoid money calculations in different currency and some other types of mistakes.
 * This is implementation of "Money" pattern suggested by Martin Fowler.
 *
 * Usage example: 
 * Money someAmount1 = new Money(500, Currency.RUR); 
 * Money someAmount2 = new Money(500, Currency.USD);
 * Money someAmount3 = new Money(100, Currency.RUR);
 * (someAmount1 == someAmount2) - returns "false"
 * (someAmount1 + someAmount2)	- throws an exception
 * (someAmount1 + ConvertToCurrency(someAmount2, Currency.RUR), 36.14) - returns correct amount of addition
 * 
 * (c) Denis Shchuka, 2014
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Domain.Common.ValueObject
{
    public class Money
    {
        #region Prop's and Variables

        public decimal Amount { get; set; }
        public int IntegralPart
        {
            get
            {
                return Convert.ToInt32(Math.Truncate(Math.Abs(Amount)));
            }
        }
        public int FractionalPart
        {
            get
            {
                if (Math.Abs(this.Amount) == this.IntegralPart)
                    return 0;

                string fractionalPart = (Math.Abs(this.Amount) - this.IntegralPart).ToString().Replace(',', '.');
                fractionalPart = fractionalPart.Substring(fractionalPart.IndexOf('.') + 1).ToString().Substring(0, 2);
                return Convert.ToInt32(fractionalPart);
            }
        }

        public Currency SelectedCurrency { get; set; }
        /// <summary>
        /// text representation of money amount
        /// </summary>
        public string Text
        {
            get
            {
                return AmountToText();
            }
        }

        /// <summary>
        /// Converts amount of money representes as number into text representation
        /// </summary>
        /// <returns>Amount of money as a text</returns>
        private string AmountToText()
        {
            string sumText = "";
            switch (this.SelectedCurrency)
            {
                case Currency.RUR:
                    if (this.IntegralPart != 0)
                        sumText += GetAmountTextRUR(this.IntegralPart) + " руб. ";
                    if (this.FractionalPart != 0)
                        sumText += GetAmountFractionalTextRUR(this.FractionalPart) + " коп.";
                    break;
                case Currency.USD:
                    if (this.IntegralPart != 0)
                        sumText += GetAmountTextEN(this.IntegralPart) + " dollars ";
                    if (this.FractionalPart != 0)
                    {
                        sumText += GetAmountFractionalTextEN(this.FractionalPart);
                        sumText += " ";
                        sumText += this.FractionalPart == 1 ? "cent" : "cents";
                    }
                    break;
                case Currency.EUR:
                    if (this.IntegralPart != 0)
                        sumText += GetAmountTextEN(this.IntegralPart) + " euro ";
                    if (this.FractionalPart != 0)
                    {
                        sumText += GetAmountFractionalTextEN(this.FractionalPart);
                        sumText += " ";
                        sumText += this.FractionalPart == 1 ? "cent" : "cents";
                    }
                    break;

                default: throw new NotImplementedException("There is no text representation converter for this currency!");
            }
            return sumText.Trim();
        }

        /// <summary>
        /// Get fractional part of an amount as a text string
        /// </summary>
        /// <param name="amount">Fractional part of an amount</param>
        /// <returns>Text representation of fractional part (2 digits after comma)</returns>
        private string GetAmountFractionalTextEN(int amount)
        {
            string amountText = "";

            string[] digits = new string[]
                {"","one","two","three","four","five","six","seven","eight","nine","ten",
                    "eleven","twelve","thirteen","fourteen","fifteen","sixteen",
                    "seventeen","eighteen","nineteen"};


            string[] tens = new string[] { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (amount < 20)
            {
                amountText = digits[amount];
            }
            else
            {
                amountText = tens[(int)amount / 10];
                amountText += " " + digits[amount % 10];
            }

            return amountText;
        }

        /// <summary>
        /// Get fractional part of an amount as a text string
        /// </summary>
        /// <param name="amount">Fractional part of an amount</param>
        /// <returns>Text representation of fractional part (2 digits after comma)</returns>
        private string GetAmountFractionalTextRUR(int amount)
        {
            string amountText = "";

            if (amount.ToString().Length > 2)
                amount = Convert.ToInt32(amount.ToString().Substring(0, 2));


            //array of digits with alternative endings for russian language
            string[] digits = new string[] { "", "одна", "две" ,"три","четыре","пять","шесть","семь","восемь","девять","десять",
                    "одинадцать","двенадцать","тринадцать","четырнадцать","пятнадцать","шестнадцать",
                    "семнадцать","восемнадцать","девятнадцать"};


            string[] tens = new string[] { "", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };


            if (amount < 20)
            {
                amountText = digits[amount];
            }
            else
            {
                amountText = tens[(int)amount / 10];
                amountText += " " + digits[amount % 10];
            }

            return amountText;
        }

        /// <summary>
        /// Get text representation of integral part of the amount
        /// </summary>
        /// <param name="amount">Money amount</param>
        /// <returns>Text representation of the amount</returns>
        private string GetAmountTextEN(int amount)
        {
            string[] digits = new string[]
                {"","one","two","three","four","five","six","seven","eight","nine","ten",
                    "eleven","twelve","thirteen","fourteen","fifteen","sixteen",
                    "seventeen","eighteen","nineteen"};


            string[] tens = new string[] { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] hundreds = new string[] { "", "one hundred", "two hundred", "three hundred", "four hundred", "five hundred", "six hundred",
                "seven hundred", "eigth hundred", "nine hundred" };

            string thousandEnding = "thousand";
            string millionEnding = "million";
            string billionEnding = "billion";



            List<string> triadsList = new List<string>(); //triads of digits from the amount 
            List<string> triadsTextList = new List<string>(); //triads as a text        


            //getting triads
            string tmpAmountText = amount.ToString(); //the remaining characters in the amount's text
            string triad = ""; //one triad

            while (tmpAmountText.Length >= 3) //full triad
            {
                triad = tmpAmountText.Substring(tmpAmountText.Length - 3);
                triadsList.Add(triad);
                tmpAmountText = tmpAmountText.Remove(tmpAmountText.Length - 3);
            }

            //getting text representation for every triad and save it in list
            //adding endings that represent triad position in the amount - (thousand, million, billion, etc.)
            foreach (string triadItem in triadsList)
            {
                string triadItemText = hundreds[Convert.ToInt32(triadItem[0].ToString())];
                if (triadItem[1] != '0')
                    triadItemText += " ";
                triadItemText += tens[Convert.ToInt32(triadItem[1].ToString())];
                if (triadItem[2] != '0')
                {
                    triadItemText += " ";
                    triadItemText += digits[Convert.ToInt32(triadItem[2].ToString())];
                }

                if (triadsTextList.Count() > 0)
                {
                    int lastNum = Convert.ToInt32(triadItem[2].ToString());
                    switch (triadsTextList.Count())
                    {
                        case 1:
                            triadItemText += " " + thousandEnding;
                            break;
                        case 2:
                            triadItemText += " " + millionEnding;
                            break;
                        case 3:
                            triadItemText += " " + billionEnding;
                            break;
                    }

                }

                triadsTextList.Add(triadItemText);
            }

            //get remaining number that hasn't been included into any triad
            string firstNum = null;
            switch (tmpAmountText.Length)
            {
                case 2:
                    firstNum = tens[Convert.ToInt32(tmpAmountText[0].ToString())];
                    firstNum += " " + digits[Convert.ToInt32(tmpAmountText[1].ToString())];

                    break;
                case 1:
                    firstNum = digits[Convert.ToInt32(tmpAmountText[0].ToString())];
                    break;
                default: break;
            }

            if (!String.IsNullOrEmpty(firstNum))
            {
                if (triadsTextList.Count() == 1) firstNum += " " + thousandEnding;
                if (triadsTextList.Count() == 2) firstNum += " " + millionEnding;
                if (triadsTextList.Count() == 3) firstNum += " " + billionEnding;
                triadsTextList.Add(firstNum);
            }

            string amountText = ""; //amount as a text
            triadsTextList.Reverse();
            foreach (var digitText in triadsTextList)
                amountText += digitText + " ";
            amountText = amountText.Trim();

            return amountText;
        }

        /// <summary>
        /// Get text representation of integral part of the amount
        /// </summary>
        /// <param name="amount">Money amount</param>
        /// <returns>Text representation of the amount</returns>
        private string GetAmountTextRUR(int amount)
        {

            string[] digits1to20 = new string[]
                {"","один","два","три","четыре","пять","шесть","семь","восемь","девять","десять",
                    "одинадцать","двенадцать","тринадцать","четырнадцать","пятнадцать","шестнадцать",
                    "семнадцать","восемнадцать","девятнадцать"};
            //массив единиц с альтернативными окончаниями
            string[] altDigits = new string[] { "", "одна", "две" ,"три","четыре","пять","шесть","семь","восемь","девять","десять",
                    "одинадцать","двенадцать","тринадцать","четырнадцать","пятнадцать","шестнадцать",
                    "семнадцать","восемнадцать","девятнадцать"};


            string[] tens = new string[] { "", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
            string[] hundreds = new string[] { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" };

            string[] thousandEndings = new string[] { "тысяч", "тысяча", "тысячи", "тысячи", "тысячи", "тысяч", "тысяч", "тысяч", "тысяч", "тысяч" };
            string[] millionEndings = new string[] { "миллионов", "миллион", "миллиона", "миллиона", "миллиона", "миллионов", "миллионов", "миллионов", "миллионов", "миллионов" };
            string[] billionEndings = new string[] { "миллиардов", "миллиард", "миллиарда", "миллиарда", "миллиарда", "миллиардов", "миллиардов", "миллиардов", "миллиардов", "миллиардов", "миллиардов" };



            List<string> triadsList = new List<string>(); //триады цифр, составляющие сумму
            List<string> triadsTextList = new List<string>(); //триады цифр текстом        


            //получаем триады
            string tmpAmountText = amount.ToString(); //оcтавшиеся символы в сумме текстом
            string triad = ""; //одна триада

            while (tmpAmountText.Length >= 3) //full triad
            {
                triad = tmpAmountText.Substring(tmpAmountText.Length - 3);
                triadsList.Add(triad);
                tmpAmountText = tmpAmountText.Remove(tmpAmountText.Length - 3);
            }

            //для каждой полной триады формируем текстовое представление и сохраняем его в списке
            //добавляем порядковое окончание для триады (тысяча, тысячи, тысяч, миллион, миллионов, etc.)
            foreach (string triadItem in triadsList)
            {
                string triadItemText = hundreds[Convert.ToInt32(triadItem[0].ToString())];
                if (triadItem[1] != '0')
                    triadItemText += " ";
                triadItemText += tens[Convert.ToInt32(triadItem[1].ToString())];
                if (triadItem[2] != '0')
                {
                    triadItemText += " ";
                    triadItemText += digits1to20[Convert.ToInt32(triadItem[2].ToString())];
                }

                if (triadsTextList.Count() > 0)
                {
                    int lastNum = Convert.ToInt32(triadItem[2].ToString());
                    switch (triadsTextList.Count())
                    {
                        case 1:
                            triadItemText += " " + thousandEndings[lastNum];
                            break;
                        case 2:
                            triadItemText += " " + millionEndings[lastNum];
                            break;
                        case 3:
                            triadItemText += " " + billionEndings[lastNum];
                            break;
                    }

                }

                triadsTextList.Add(triadItemText);
            }

            //получаем не вошедшее в триаду число
            string firstNum = null;
            switch (tmpAmountText.Length)
            {
                case 2:
                    firstNum = tens[Convert.ToInt32(tmpAmountText[0].ToString())];
                    firstNum += " " + altDigits[Convert.ToInt32(tmpAmountText[1].ToString())];

                    break;
                case 1:
                    firstNum = altDigits[Convert.ToInt32(tmpAmountText[0].ToString())];
                    break;
                default: break;
            }

            if (!String.IsNullOrEmpty(firstNum))
            {
                if (triadsTextList.Count() == 1) firstNum += " " + thousandEndings[Convert.ToInt32(tmpAmountText[tmpAmountText.Length - 1].ToString())];
                if (triadsTextList.Count() == 2) firstNum += " " + millionEndings[Convert.ToInt32(tmpAmountText[tmpAmountText.Length - 1].ToString())];
                if (triadsTextList.Count() == 3) firstNum += " " + billionEndings[Convert.ToInt32(tmpAmountText[tmpAmountText.Length - 1].ToString())];
                triadsTextList.Add(firstNum);
            }

            string amountText = ""; //сумма прописью
            triadsTextList.Reverse();
            foreach (var digitText in triadsTextList)
                amountText += digitText + " ";
            amountText = amountText.Trim();

            return amountText;
        }

        #endregion

        #region CTOR's
        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.SelectedCurrency = currency;
        }
        public Money(int amount, Currency currency)
            : this(Convert.ToDecimal(amount), currency)
        {
        }
        public Money(double amount, Currency currency)
            : this(Convert.ToDecimal(amount), currency)
        {
        }
        #endregion

        #region Comprasion operators
        public static bool operator ==(Money firstValue, Money secondValue)
        {
            if ((object)firstValue== null || (object)secondValue == null)
                return false;

            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency) return false;
            return firstValue.Amount == secondValue.Amount;
        }


        public static bool operator !=(Money firstValue, Money secondValue)
        {
            return !(firstValue == secondValue);
        }

        public static bool operator >(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
                throw new InvalidOperationException("Comprasion between different currencies is not allowed.");

            return firstValue.Amount > secondValue.Amount;
        }

        public static bool operator <(Money firstValue, Money secondValue)
        {
            if (firstValue == secondValue) return false;

            return !(firstValue > secondValue);
        }

        public static bool operator <=(Money firstValue, Money secondValue)
        {
            if (firstValue < secondValue || firstValue == secondValue) return true;

            return false;
        }

        public static bool operator >=(Money firstValue, Money secondValue)
        {
            if (firstValue > secondValue || firstValue == secondValue) return true;

            return false;
        }

        #endregion

        #region Ariphmetical operations
        public static Money operator +(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount + secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator -(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount - secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator *(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount * secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator /(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount / secondValue.Amount, firstValue.SelectedCurrency);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts monetary value to specified currency
        /// </summary>
        /// <param name="sourceValue">Value that should be converted</param>
        /// <param name="destinationCurrency">Destination currency</param>
        /// <param name="exchangeRate">Exchange rate - source value will be multiplied on this one</param>
        /// <returns>Converted monetary value</returns>
        public static Money ConvertToCurrency(Money sourceValue, Currency destinationCurrency, double exchangeRate)
        {
            if (sourceValue == null || exchangeRate <= 0)
                throw new InvalidCastException("Wrong amount or exchange rate");

            return new Money(sourceValue.Amount * (decimal)exchangeRate, destinationCurrency);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            Money money = obj as Money;
            return (this.Amount == money.Amount && this.SelectedCurrency == money.SelectedCurrency);
        }

        public bool Equals(Money money)
        {
            if ((object)money == null) return false;

            return (this.Amount == money.Amount && this.SelectedCurrency == money.SelectedCurrency);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return this.Amount.ToString();
        }

        #endregion

    }


    /// <summary>
    /// 3-symb representation of currency
    /// </summary>
    public enum Currency
    {
        RUR, USD, EUR
    }

}
