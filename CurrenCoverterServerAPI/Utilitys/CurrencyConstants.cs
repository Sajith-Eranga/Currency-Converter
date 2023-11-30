namespace CurrenCoverterServerAPI.Utilitys
{
    public static class CurrencyConstants
    {
        public static string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        public static string[] teens = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        public static string[] tens = { "", "ten", "twenty-", "thirty-", "forty-", "fifty-", "sixty-", "seventy-", "eighty-", "ninety-" };
    }

    public static class ConvertExceptions
    {
        public static string formatException = "System.FormatException";
        public static string indexOutOfRangeException = "System.IndexOutOfRangeException";
        public static string overflowException = "System.OverflowException";
    }
    public static class ExceptionsMessages
    {
        public static string formatExceptionMsg = "Input format is incorrect. Correct format is e.g: 0,01 \nThe separator between dollars and cents is a ‘,’ (comma)";
        public static string indexOutOfRangeExceptionMsg = "Input format is incorrect. Correct format is e.g: 0,01 \nThe separator between dollars and cents is a ‘,’ (comma)";
        public static string overflowExceptionMsg = "The maximum number of dollars is 999 999 999";
        public static string minCentCurrencyMsg = "Input format is incorrect. You can enter only 2 decimal places for cents";
        public static string minusCurrencyMsg= "Input format is incorrect. You cannot enter minus values";
    }
}
