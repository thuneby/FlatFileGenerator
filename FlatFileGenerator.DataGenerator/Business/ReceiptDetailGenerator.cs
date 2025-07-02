using System.Collections.Concurrent;
using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.DataGenerator.Business
{
    public class ReceiptDetailGenerator
    {
        private static readonly DateTime Today = DateTime.Today;
        private readonly DateTime _firstOfMonth = new DateTime(Today.Year, Today.Month, 1);
        private readonly DateTime _lastOfMonth = GetLastOfMonth(Today);
        
        private ReceiptDetail GenerateReceiptDetail()
        {
            var today = DateTime.Today;

            var r = new ReceiptDetail
            {
                ReceivedDate = DateTime.Today,
                Amount = AmountGenerator.GetAmount(),
                Cpr = CprGenerator.GetCprModulus(true),
                Cvr = CprGenerator.GetCvr(),
                PersonFullName = NameGenerator.GetName(),
                FromDate = _firstOfMonth,
                ToDate = _lastOfMonth,
                PaymentDate = Today.AddDays(7),
                ReceiptType = ReceiptType.Payment,
                PaymentReference = "INFO-OVF"
            };
            return r;
        }

        public List<ReceiptDetail> GenerateReceiptDetails(int count = 100)
        {
            var bag = new ConcurrentBag<ReceiptDetail>();

            Parallel.For(0, count, i =>
            {
                bag.Add(GenerateReceiptDetail());
            });
            return bag.ToList();
        }

        private static DateTime GetLastOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }
    }
}
