using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models;

namespace FlatFileGenerator.FileWriter.Business.Helpers
{
    public class CreateNetsIs
    {
        public static (InfoStart, decimal) CreateNetsModel(List<ReceiptDetail> recordList, string batchNumber,
            string bankAccount)
        {
            var batchDate = DateTime.Today.ToString("yyyyMMdd");
            var infoStart = NetsIsModelHelper.GetInfoStart(batchNumber, batchDate);
            var totalRecords = 0;
            var totalLines = 0;
            var totalAmount = 0.0M;

            var groupByDatesList = recordList.GroupBy(x => x.PaymentDate)
                .Select(g => new
                {
                    BankTrxDate = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var totalSections = groupByDatesList.Count;
            var sectionNumber = 0;

            foreach (var bankDate in groupByDatesList)
            {
                var sectionAmount = 0.0M;
                sectionNumber++;
                var firstDate = recordList.OrderBy(x => x.ReceivedDate).Select(x => x.ReceivedDate).FirstOrDefault();

                var sectionStart = NetsIsModelHelper.GetInfoSectionStart(batchNumber, sectionNumber, firstDate, bankDate.BankTrxDate, bankAccount);
                var transfers = recordList.Where(x => x.PaymentDate == bankDate.BankTrxDate).ToList();
                var record00Count = 0;
                var sectionRecords = 0;
                foreach (var transfer in transfers)
                {
                    record00Count++;
                    var record00 = GetInfoRecord00Model(batchNumber, sectionStart, record00Count, batchDate, transfer, out var transferRecordCount);
                    sectionAmount += transfer.Amount;
                    sectionRecords += transferRecordCount;
                    sectionStart.Record00Records.Add(record00);
                }

                var sectionLines = record00Count;
                var sectionEnd = NetsIsModelHelper.GetInfoSectionEnd(batchNumber, sectionStart, sectionRecords, sectionLines, sectionAmount);
                sectionStart.InfoSectionEnd.Add(sectionEnd);
                infoStart.InfoSectionStartRecords.Add(sectionStart);
                totalRecords += sectionRecords;
                totalLines += sectionLines;
                totalAmount += sectionAmount;
            }

            var infoEnd = NetsIsModelHelper.GetInfoEnd(batchNumber, batchDate, totalRecords, totalSections, totalLines, totalAmount);
            infoStart.InfoEnd.Add(infoEnd);
            return (infoStart, totalAmount);
        }

        private static InfoRecord00 GetInfoRecord00Model(string batchNumber, InfoSectionStart sectionStart, int record00Count, string batchDate, ReceiptDetail transfer, out int transferRecordCount)
        {
            var record00 = NetsIsModelHelper.GetInfoRecord00(batchNumber, sectionStart, record00Count, batchDate, transfer);
            var record01 = NetsIsModelHelper.GetInfoRecord01(batchNumber, sectionStart, transfer, record00);
            var record02 = NetsIsModelHelper.GetInfoRecord02(batchNumber, sectionStart, transfer, record00);
            record00.InfoRecord01.Add(record01);
            record00.InfoRecord02.Add(record02);
            transferRecordCount = 1 + record00.InfoRecord01.Count + record00.InfoRecord02.Count;
            record00.REC_ANT = transferRecordCount.ToString("D2");
            return record00;
        }

        public static byte[] CreatePayload(InfoStart netsModel)
        {
            throw new NotImplementedException();
        }
    }
}
