using System.Text;
using FileHelpers;
using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using FlatFileGenerator.FileWriter.Business.Mappers;
using Microsoft.Extensions.Logging;

namespace FlatFileGenerator.FileWriter.Business.Helpers
{
    public class CreateNetsIs
    {
        private readonly InfoStartMapper _infoStartMapper;
        private readonly InfoEndMapper _infoEndMapper;
        private readonly InfoSectionStartMapper _infoSectionStartMapper;
        private readonly InfoSectionEndMapper _infoSectionEndMapper;
        private readonly InfoRecord00Mapper _infoRecord00Mapper;
        private readonly InfoRecord01Mapper _infoRecord01Mapper;
        private readonly InfoRecord02Mapper _infoRecord02Mapper;

        public CreateNetsIs(ILoggerFactory loggerFactory)
        {
            _infoStartMapper = new InfoStartMapper(loggerFactory);
            _infoEndMapper = new InfoEndMapper(loggerFactory);
            _infoSectionStartMapper = new InfoSectionStartMapper(loggerFactory);
            _infoSectionEndMapper = new InfoSectionEndMapper(loggerFactory);
            _infoRecord00Mapper = new InfoRecord00Mapper(loggerFactory);
            _infoRecord01Mapper = new InfoRecord01Mapper(loggerFactory);
            _infoRecord02Mapper = new InfoRecord02Mapper(loggerFactory);
        }

        public (InfoStart, decimal) CreateNetsModel(List<ReceiptDetail> recordList, string batchNumber,
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

        public byte[] CreatePayload(InfoStart netsModel)
        {
            var records = GetNetsBaseRecords(netsModel);
            return CreatePayloadfromNetsRecords(records);
        }

        private IEnumerable<NetsBase> GetNetsBaseRecords(InfoStart netsModel)
        {
            var records = new HashSet<NetsBase> { _infoStartMapper.GetRecord(netsModel) };
            foreach (var section in netsModel.InfoSectionStartRecords)
            {
                records.Add(_infoSectionStartMapper.GetRecord(section));
                foreach (var innnerRecord in GetInnerRecords(section))
                {
                    records.Add(innnerRecord);
                }
                records.Add(_infoSectionEndMapper.GetRecord(section.InfoSectionEnd.FirstOrDefault()));
            }
            records.Add(_infoEndMapper.GetRecord(netsModel.InfoEnd.FirstOrDefault()));
            return records;
        }

        private IEnumerable<NetsBase> GetInnerRecords(InfoSectionStart section)
        {
            var baseRecords = new HashSet<NetsBase>();
            foreach (var record00 in section.Record00Records)
            {
                baseRecords.Add(_infoRecord00Mapper.GetRecord(record00));
                foreach (var i01 in record00.InfoRecord01)
                    baseRecords.Add(_infoRecord01Mapper.GetRecord(i01));
                foreach (var i02 in record00.InfoRecord02)
                    baseRecords.Add(_infoRecord02Mapper.GetRecord(i02));
            }
            return baseRecords;
        }


        private static byte[] CreatePayloadfromNetsRecords(IEnumerable<NetsBase> records)
        {
            var engine = new MultiRecordEngine(typeof(InfoStartRecord),
                    typeof(InfoSectionStartRecord), typeof(InfoRecordFixed00),
                    typeof(InfoRecordFixed01), typeof(InfoRecordFixed02),
                    typeof(InfoRecordFixed03), typeof(InfoRecordFixed04),
                    typeof(InfoRecordFixed05), typeof(InfoRecordFixed10),
                    typeof(InfoSectionEndRecord), typeof(InfoEndRecord))
                { Encoding = Encoding.Default };

            byte[] result;
            using var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, Encoding.Default) { AutoFlush = true };
            engine.WriteStream(streamWriter, records);
            result = stream.ToArray();
            return result;
        }
    }
}
