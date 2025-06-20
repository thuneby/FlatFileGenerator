using System.Globalization;

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

        public static DateTime ParseDate(string dateString)
        {
            try
            {
                return DateTime.ParseExact(dateString, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch
            {
                return GetMinDateTime();
            }
        }

        public static bool IsMinDateTime(DateTime date)
        {
            return date == GetMinDateTime();
        }

        private static DateTime GetMinDateTime()
        {
            return new DateTime(1900, 1, 1);
        }
    }
}
