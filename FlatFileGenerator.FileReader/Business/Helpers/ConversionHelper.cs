using System.Globalization;
using System.Text;

namespace FlatFileGenerator.FileReader.Business.Helpers
{
    internal static class ConversionHelper
    {
        public static decimal GetDecimal100(string inputString)
        {
            var success = decimal.TryParse(inputString, out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return result / 100;
        }

        public static decimal GetDecimal100(string inputString, string sign)
        {
            var success = decimal.TryParse(inputString, out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return sign == "-" || sign == "2" ? -result / 100 : result / 100;
        }

        public static decimal GetDecimal1000(string inputString)
        {
            var success = decimal.TryParse(inputString, out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return result / 1000;
        }

        public static decimal GetDecimal10000(string? inputString)
        {
            var success = decimal.TryParse(inputString, out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return result / 10000;
        }

        public static decimal GetDecimalLong(long? input)
        {
            if (input == null)
                return 0M;
            return input.Value / 100M;
        }

        public static DateTime ParseDate(string? dateString)
        {
            return ParseDateNull(dateString) ?? GetMinDateTime();
        }

        public static DateTime? ParseDate10(string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;
            try
            {
                return DateTime.ParseExact(dateString, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime? ParseDateNull(string? dateString, bool allowNull = false)
        {
            if (string.IsNullOrWhiteSpace(dateString) || dateString == "00000000")
                return allowNull ? null : GetMinDateTime();
            try
            {
                return DateTime.ParseExact(dateString, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch
            {
                if (allowNull)
                    return null;
                return GetMinDateTime();
            }
        }

        public static DateTime? ParseDate6YMD(string? dateString)
        {
            if (dateString == null)
                return null;
            try
            {
                return DateTime.ParseExact(dateString, "yyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime? ParseMonthYYMM(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString) || dateString.Length != 4 || !int.TryParse(dateString, out var datenumber))
                return null;
            if (datenumber == 0)
                return null;
            var year = int.Parse(dateString.Substring(0, 2));
            var month = int.Parse(dateString.Substring(2, 2));
            return year + 2000 > DateTime.Today.Year ? new DateTime(year + 1900, month, 1) : new DateTime(year + 2000, month, 1);
        }

        public static DateTime? ParseMonthMMYY(string? dateString)
        {
            if (string.IsNullOrEmpty(dateString) || dateString.Length != 4 || !int.TryParse(dateString, out _))
                return null;
            var month = int.Parse(dateString.Substring(0, 2));
            var year = int.Parse(dateString.Substring(2, 2));
            return year + 2000 > DateTime.Today.Year
                ? new DateTime(year + 1900, month, 1)
                : new DateTime(year + 2000, month, 1);
        }


        public static bool IsMinDateTime(DateTime date)
        {
            return date == GetMinDateTime();
        }

        public static DateTime GetMinDateTime()
        {
            return new DateTime(1900, 1, 1);
        }

        public static DateTime GetMonthStart(DateTime date, bool lastMonth = true)
        {
            var monthStart = new DateTime(date.Year, date.Month, 1);
            if (date.Day <= 15) return lastMonth ? monthStart.AddMonths(-1) : monthStart;
            return lastMonth ? monthStart : monthStart.AddMonths(1);
        }

        public static DateTime GetMonthStartSimple(DateTime date)
        {
            var monthStart = new DateTime(date.Year, date.Month, 1);
            return monthStart;
        }


        public static DateTime GetMonthEnd(DateTime date, bool lastMonth = true)
        {
            var start = GetMonthStart(date, lastMonth);
            return start.AddMonths(1).AddDays(-1);
        }


        public static DateTime GetMonthEndSimple(DateTime date)
        {
            var start = GetMonthStartSimple(date);
            return start.AddMonths(1).AddDays(-1);
        }

        public static string CprHelper(string? textCpr)
        {
            if (textCpr == null)
                return string.Empty;
            textCpr = textCpr.Trim();
            if (textCpr.Length == 10)
                textCpr = textCpr.Substring(0, 6) + "-" + textCpr.Substring(6, 4);
            return textCpr;
        }

        public static string Concatenate(string s1, string s2)
        {
            return s1.Trim() + s2.Trim(); ;
        }

        public static decimal GetDecimalUs(string? amount, string sign)
        {
            const NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign;
            var success = decimal.TryParse(amount, style, CultureInfo.GetCultureInfo("en-US"), out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return sign is "-" or "2" ? -result : result;
        }

        public static bool StringContainsValue(string codeString)
        {
            return codeString != null && codeString.Trim().Length != 0;
        }

        public static bool StringContainsPositiveNumber(string codeString)
        {
            return codeString != null && !string.IsNullOrWhiteSpace(codeString.Trim('0'));
        }

        public static string RandomString(int length)
        {
            var builder = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < length; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString().ToLower();
        }

    }
}
