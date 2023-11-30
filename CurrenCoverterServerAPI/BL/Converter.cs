using CurrenCoverterServerAPI.Utilitys;
using System.Diagnostics.Metrics;
using System.Text;

namespace CurrenCoverterServerAPI.BL
{
    public class Converter
    {

        #region Main public method for Coverting currency to words
        /// <methodname>
        /// public string ConvertCurrencyToWords(string number)
        /// </methodname>
        /// <summary>
        /// Convert currency numbers to words
        /// </summary>
        /// <param name="number">User entered currency number</param>  
        /// <returns></returns>
        /// <revision>0</revision>
        /// <author>Sajith</author>
        /// <datecreated>2023-11-27</datecreated>
        ///<lastmodifiedby></lastmodifiedby>
        ///<lastdatemodified></lastdatemodified>
        ///<lastmodifiedreason></lastmodifiedreason>
        public string ConvertCurrencyToWords(string number)
        {
            string centsWord = string.Empty;
            string validationMsg = string.Empty;
            string dollarsInWords = string.Empty;
            try
            {
                // split the entered input to 2 string arrays as dollars and cents arrays.
                string[] parts = number.Split(',');
                if (parts.Length > 2)
                {
                    return ExceptionsMessages.formatExceptionMsg;
                }
                int dollars = int.Parse(parts[0].Replace(" ", ""));
                dollarsInWords = CDtoWords(dollars);

                // only if the user has entered cents values below if condition will be executed.
                if (parts.Length > 1)
                {
                    parts[1] = parts[1].Trim().Replace(" ", "");
                    int cents = int.Parse(parts[1].Replace(" ", ""));
                    if (parts[1].Length == 1 || cents < 10)
                    {
                        parts[1] = parts[1] + "0";
                        if (parts[1].Length == 2)
                        {
                            cents = cents*10;
                        }
                    }
                    // If the user has entered more digits (more than 2) below if condition will be executed and return an error message.
                    if (parts[1].Length > 3 || cents > 99)
                    {
                        return ExceptionsMessages.minCentCurrencyMsg;
                    }
                    centsWord = CToWords(cents);
                    return $"{dollarsInWords} and {centsWord}";
                }               
            }
            catch (Exception exception)
            {   
                // validations handling
                if (ConvertExceptions.formatException.Equals(exception.GetType().FullName, StringComparison.OrdinalIgnoreCase))
                    return ExceptionsMessages.formatExceptionMsg;
                if (ConvertExceptions.indexOutOfRangeException.Equals(exception.GetType().FullName, StringComparison.OrdinalIgnoreCase))
                    return ExceptionsMessages.formatExceptionMsg;
                if (ConvertExceptions.overflowException.Equals(exception.GetType().FullName, StringComparison.OrdinalIgnoreCase))
                    return ExceptionsMessages.overflowExceptionMsg;
                else
                {
                    return ExceptionsMessages.formatExceptionMsg;
                }
            }

            return $"{dollarsInWords}";
        }
        #endregion

        #region Generate 0,1,thousands and millions
        private string CDtoWords(int dollars)
        {
            // return an error message for minus values
            if(dollars < 0)
            {
                return ExceptionsMessages.minusCurrencyMsg;
            }
            else if (dollars == 0)
            {
                return "zero dollars";
            }
            else if(dollars == 1)
            {
                return "one dollar";
            }

            string result = "";
            // generate millions
            if (dollars >= 1000000)
            {
                result += CThousandDToWord(dollars / 1000000) + " million ";
                dollars %= 1000000;
            }

            // generate thousands
            if (dollars >= 1000)
            {
                result += CThousandDToWord(dollars / 1000) + " thousand ";
                dollars %= 1000;
            }
            // Generate hundreds, tens and ones ( 1 - 999)
            if (dollars > 0)
            {
                result += CThousandDToWord(dollars) + " dollars ";
            }
            else
            {
                result += "dollars ";
            }

            return result.ToString().Trim();
        }
        #endregion 

        #region Generate hundreds, tens and ones ( 1 - 999)
        static string CThousandDToWord(int num)
        {
            string result = "";

            if (num >= 100)
            {
                result += CurrencyConstants.ones[num / 100] + " hundred";
                num %= 100;
            }

            if (num >= 11 && num <= 19)
            {
                result += " " + CurrencyConstants.teens[num - 11];
                return result.ToString();
            }
            else if (num >= 20 || num == 10)
            {
                result += " " + CurrencyConstants.tens[num / 10];
                num %= 10;
            }
            if (num > 0)
            {
                result += "" + CurrencyConstants.ones[num];
            }
            else if(result.EndsWith("-"))
            {
                result = result.Remove(result.Length - 1, 1);
            }

            return result.ToString().Trim();
        }
        #endregion

        #region Cents to Words
        static string CToWords(int cents)
        {
            if (cents == 0)
            {
                return "zero cents";
            }
            else if (cents == 1)
            {
                return "one cent";
            }

            string result = CHundradDToWords(cents) + " cents";

            return result;
        }
        #endregion

        #region 1-99 Cents to Word
        static string CHundradDToWords(int num)
        {
            string result = "";

            if (num >= 11 && num <= 19)
            {
                result += " " + (CurrencyConstants.teens[num - 11]);
            }
            else if (num >= 20)
            {
                result += " " + CurrencyConstants.tens[num / 10];
                num %= 10;
            }

            if (num > 0)
            {
                if(num == 10)
                {
                    result += "" + CurrencyConstants.tens[num / 10];
                    num %= 10;
                }
                result += "" + CurrencyConstants.ones[num];
            }
            else
            {
                result = result.Remove(result.Length - 1, 1);
            }

            return result.ToString().Trim();
        }
        #endregion

    }
}
