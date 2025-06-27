using System.Runtime.ConstrainedExecution;
using static System.Text.RegularExpressions.Regex;

namespace FlatFileGenerator.DataGenerator.Business
{
    public static class CprGenerator 
    {
        private static readonly Random Random = new();
        private static readonly DateTime Start = new DateTime(1955, 1, 1);
        private static readonly DateTime End = new DateTime(2005, 12, 31);
        private static readonly int Range = (End - Start).Days;
        private static readonly string[] CvrList = ["24256790", "61056416", "25133455", "56759514", "56759514", "26460212", "61126228", "36213728"];
        
        public static List<string> Generate(int count = 100, bool addDash = false)
        {
            var result = new List<string>();
            for (var i = 0; i < count; i++)
            {
                var s = GetCpr(addDash);
                result.Add(s);
            }
            return result;
        }

        public static string GetCpr(bool addDash = false)
        {
            var s = RandomDateString(Range, addDash);
            s += Random.Next(0, 9999).ToString("D4");
            return s;
        }

        public static string GetCprModulus(bool addDash = false)
        {
            var s = RandomDateString(Range);
            var s2 = Random.Next(0, 999).ToString("D3");
            var modulo = GetModulo(s+s2);
            if (addDash)
                s += '-';
            switch (modulo)
            {
                case 0:
                    return s+s2+'0';
                case > 1:
                {
                    var digit = 11 - modulo;
                    return s + s2 + digit;
                }
                default:
                    s += Random.Next(0, 9999).ToString("D4");
                    return s;
            }
        }

        public static string GetCvr() 
        {
            var index = Random.Next(CvrList.Length);
            return CvrList[index];
        }

        private static string RandomDateString(int range, bool addDash = false)
        {
            var date = RandomDay(range);
            var stringDate = date.Day.ToString("D2") + date.Month.ToString("D2") + GetYear2Digits(date.Year);
            if (addDash)
            {
                stringDate += "-";
            }
            return stringDate;
        }

        private static string GetYear2Digits(int dateYear)
        {
            return dateYear.ToString().Substring(2, 2);
        }

        private static DateTime RandomDay(int range)
        {
            return Start.AddDays(Random.Next(range));
        }

        public static bool IsValidCpr(string cpr) 
        {
            cpr = Replace(cpr, @"[-]", "");
            if (cpr.Length != 10)
                 return false; 
            var j = int.Parse(cpr.Substring(9, 1));
            var result = (GetModulo(cpr) + j) % 11;
            return result == 0;
        }

        private static int GetModulo(string cpr)
        {
            var a = int.Parse(cpr.Substring(0, 1));
            var b = int.Parse(cpr.Substring(1, 1));
            var c = int.Parse(cpr.Substring(2, 1));
            var d = int.Parse(cpr.Substring(3, 1));
            var e = int.Parse(cpr.Substring(4, 1));
            var f = int.Parse(cpr.Substring(5, 1));
            var g = int.Parse(cpr.Substring(6, 1));
            var h = int.Parse(cpr.Substring(7, 1));
            var i = int.Parse(cpr.Substring(8, 1));
            var result = (4 * a + 3 * b + 2 * c + 7 * d + 6 * e + 5 * f + 4 * g + 3 * h + 2 * i) % 11;
            return result;
        }
    }
}
