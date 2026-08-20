using FileHelpers;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers.ReceiptDetailMappers;
using FlatFileGenerator.FileReader.Business.Mappers.TextMappers;
using FlatFileGenerator.FileReader.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace FlatFileGenerator.FileReader.Business
{
    public class NetsIsParser(ILoggerFactory loggerFactory): IAsyncParser
    {
        private readonly InfoStartMapper _infoStartMapper = new InfoStartMapper(loggerFactory);
        private readonly InfoEndMapper _infoEndMapper = new InfoEndMapper(loggerFactory);
        private readonly InfoSectionStartMapper _infoSectionStartMapper = new InfoSectionStartMapper(loggerFactory);
        private readonly InfoSectionEndMapper _infoSectionEndMapper = new InfoSectionEndMapper(loggerFactory);
        private readonly InfoRecord00Mapper _infoRecord00Mapper = new InfoRecord00Mapper(loggerFactory);
        private readonly InfoRecord01Mapper _infoRecord01Mapper = new InfoRecord01Mapper(loggerFactory);
        private readonly InfoRecord02Mapper _infoRecord02Mapper = new InfoRecord02Mapper(loggerFactory);
        private readonly InfoRecord03Mapper _infoRecord03Mapper = new InfoRecord03Mapper(loggerFactory);
        private readonly InfoRecord04Mapper _infoRecord04Mapper = new InfoRecord04Mapper(loggerFactory);
        private readonly InfoRecord05Mapper _infoRecord05Mapper = new InfoRecord05Mapper(loggerFactory);
        private readonly InfoRecord10Mapper _infoRecord10Mapper = new InfoRecord10Mapper(loggerFactory);

        public async Task<IEnumerable<ReceiptDetail>> ParseAsync(Stream payload, DocumentType documentType)
        {
            var errors = new HashSet<string>();
            var engine = new MultiRecordEngine(typeof(InfoStartRecord),
                    typeof(InfoSectionStartRecord), typeof(InfoRecordFixed00),
                    typeof(InfoRecordFixed01), typeof(InfoRecordFixed02),
                    typeof(InfoRecordFixed03), typeof(InfoRecordFixed04),
                    typeof(InfoRecordFixed05), typeof(InfoRecordFixed10),
                    typeof(InfoRecordFixed11), typeof(InfoRecordFixed12),
                    typeof(InfoRecordFixed13), typeof(InfoRecordFixed14),
                    typeof(InfoRecordFixed15), typeof(InfoRecordFixed16),
                    typeof(InfoSectionEndRecord),
                    typeof(InfoEndRecord))
                { RecordSelector = NetsIsSelector };
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            var result = engine.ReadStream(new StreamReader(payload, ParserHelperBase.GetEncoding(DocumentType.NetsIs)));
            if (result.Length == 0)
            {
                var exception = new ArgumentException("Fejl - Formatet er ikke Nets IS!");
                throw exception;
            }
            if (engine.ErrorManager.HasErrors)
                foreach (var error in engine.ErrorManager.Errors)
                {
                    errors.Add("Fejl i linie: " + error.LineNumber + " - " + error.ExceptionInfo.Message);
                }
            if (errors.Any())
            {
                var exception = new Exception(errors.FirstOrDefault());
                throw exception;
            }

            var netsModel = new InfoStart();
            var currentSection = new InfoSectionStart();
            var currentRecord00 = new InfoRecord00();

            foreach (var record in result)
            {
                var type = NetsIsParserHelper.NetsRecordTypeDictionary.GetValueOrDefault(record.GetType());
                switch (type)
                {
                    case NetsIsRecordType.StartRecord:
                        netsModel = _infoStartMapper.GetRecord(record);
                        break;
                    case NetsIsRecordType.SectionStart:
                        var sectionStart = _infoSectionStartMapper.GetRecord(record);
                        sectionStart.InfoStartId = netsModel.Id;
                        netsModel.InfoSectionStartRecords.Add(sectionStart);
                        currentSection = sectionStart;
                        break;
                    case NetsIsRecordType.Record00:
                        var infoRecord = _infoRecord00Mapper.GetRecord(record);
                        infoRecord.InfoSectionStartId = currentSection.Id;
                        currentRecord00 = infoRecord;
                        currentSection.Record00Records.Add(currentRecord00);
                        break;
                    case NetsIsRecordType.Record01:
                        var record01 = _infoRecord01Mapper.GetRecord(record);
                        record01.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord01.Add(record01);
                        break;
                    case NetsIsRecordType.Record02:
                        var record02 = _infoRecord02Mapper.GetRecord(record);
                        record02.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord02.Add(record02);
                        break;
                    case NetsIsRecordType.Record03:
                        var record03 = _infoRecord03Mapper.GetRecord(record);
                        record03.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord03.Add(record03);
                        break;
                    case NetsIsRecordType.Record04:
                        var record04 = _infoRecord04Mapper.GetRecord(record);
                        record04.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord04.Add(record04);
                        break;
                    case NetsIsRecordType.Record05:
                        var record05 = _infoRecord05Mapper.GetRecord(record);
                        record05.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord05.Add(record05);
                        break;
                    case NetsIsRecordType.Record10:
                        var record10 = _infoRecord10Mapper.GetRecord(record);
                        record10.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord10.Add(record10);
                        break;
                    case NetsIsRecordType.Record11:
                    case NetsIsRecordType.Record12:
                    case NetsIsRecordType.Record13:
                    case NetsIsRecordType.Record14:
                    case NetsIsRecordType.Record15:
                    case NetsIsRecordType.Record16:
                        break;
                    case NetsIsRecordType.SectionEnd:
                        var sectionEnd = _infoSectionEndMapper.GetRecord(record);
                        sectionEnd.InfoSectionStartId = currentSection.Id;
                        currentSection.InfoSectionEnd.Add(sectionEnd);
                        break;
                    case NetsIsRecordType.EndRecord:
                        var infoend = _infoEndMapper.GetRecord(record);
                        infoend.InfoStartId = netsModel.Id;
                        netsModel.InfoEnd.Add(infoend);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var receiptDetails = new ConcurrentBag<ReceiptDetail>();
            var receiptDetailMapper = new InfoRecordMapper(loggerFactory);
            var minimumMapper = new MinimumReceiptDetailMapper(loggerFactory);
            //Parallel.ForEach(netsModel.InfoSectionStartRecords, startRecord =>
            foreach (var startRecord in netsModel.InfoSectionStartRecords)
            {
                //Parallel.ForEach(startRecord.Record00Records, record =>
                foreach (var record in startRecord.Record00Records)
                {
                    MapReceiptDetail(receiptDetailMapper, record, startRecord, receiptDetails, minimumMapper);
                }
                //});
                //});
            }

            if (!string.IsNullOrWhiteSpace(netsModel.LEV_DTO))
            {
                Parallel.ForEach(receiptDetails, receiptDetail =>
                {
                    receiptDetail.SubmissionDate = ConversionHelper.ParseDate(netsModel.LEV_DTO);
                });
            }
            return receiptDetails.ToList();
        }

        private static void MapReceiptDetail(InfoRecordMapper receiptDetailMapper, InfoRecord00 record,
    InfoSectionStart startRecord, ConcurrentBag<ReceiptDetail> receiptDetails, MinimumReceiptDetailMapper mapper)
        {
            var receiptDetail = receiptDetailMapper.Map(record);
            if (receiptDetail == null)
                return;
            receiptDetail.PaymentDate = ConversionHelper.ParseDate(startRecord.INDBET_DTO);
            var record04 = record.InfoRecord04.FirstOrDefault();
            if (record.InfoRecord04.FirstOrDefault() != null && (HasValue(record04!.REGU_BLB1)
                                                                 || HasValue(record04.REGU_BLB2)
                                                                 || HasValue(record04.REGU_BLB3)))
            {
                var adjustments = AddAdjustment(record, receiptDetail, mapper);
                foreach (var adjustment in adjustments)
                {
                    receiptDetails.Add(adjustment);
                }
            }
            //receiptDetail.RawDataJson = JsonSerializer.Serialize(record, options);
            receiptDetails.Add(receiptDetail);
        }

        private static List<ReceiptDetail> AddAdjustment(InfoRecord00 record, ReceiptDetail receiptDetail, MinimumReceiptDetailMapper mapper)
        {
            var receiptDetails = new List<ReceiptDetail>();
            var adjustments = GetAdjustments(record.InfoRecord04.First());
            var count = adjustments.Count;
            if (count == 0)
                return receiptDetails;
            var result = new ReceiptDetail[count];
            var adjSum = adjustments.Sum(x => x.Amount);
            var totalAmount = receiptDetail.Amount;
            for (var i = 0; i < adjustments.Count; i++)
            {
                var adjReceipt = mapper.Map(receiptDetail);
                adjReceipt.Amount = adjustments[i].Amount;
                adjReceipt.ReceiptType = GetReceiptType(adjustments[i].AdjustmentCode);
                adjReceipt.FromDate = adjustments[i].FromDate;
                adjReceipt.ToDate = adjustments[i].ToDate;
                //adjReceipt.ContributorreceivablevoucherUid = receiptDetail.ContributorreceivablevoucherUid;
                result[i] = adjReceipt;
            }

            if (Math.Abs(totalAmount - adjSum) > 0.01M)
            {
                receiptDetail.Amount = totalAmount - adjSum; // The amount paid is the total amount, the rest is adjustments
            }
            else // The receiptdetail consists of adjustments only. the last adjustment is replaced by the receiptDetail
            {
                count--;
                receiptDetail.Amount = totalAmount + adjustments[count].Amount - adjSum;
                receiptDetail.ReceiptType = GetReceiptType(adjustments[count].AdjustmentCode);
                receiptDetail.FromDate = adjustments[count].FromDate;
                receiptDetail.ToDate = adjustments[count].ToDate;
            }

            for (int i = 0; i < count; i++)
            {
                receiptDetails.Add(result[i]);
            }
            return receiptDetails;
        }

        private static List<Adjustment> GetAdjustments(InfoRecord04 record04)
        {
            var adjustments = new List<Adjustment>();
            if (HasValue(record04.REGU_BLB1))
            {
                var adj1 = CreateAdjustment(record04.REGU_BLB1, record04.REGU_FRTFLT1,
                    record04.REGU_PER_FRA_DTO1, record04.REGU_PER_TIL_DTO1, record04.REGU_KOD1);
                adjustments.Add(adj1);
            }
            if (HasValue(record04.REGU_BLB2))
            {
                var adj1 = CreateAdjustment(record04.REGU_BLB2, record04.REGU_FRTFLT2,
                    record04.REGU_PER_FRA_DTO2, record04.REGU_PER_TIL_DTO2, record04.REGU_KOD2);
                adjustments.Add(adj1);
            }
            if (HasValue(record04.REGU_BLB3))
            {
                var adj1 = CreateAdjustment(record04.REGU_BLB3, record04.REGU_FRTFLT3,
                    record04.REGU_PER_FRA_DTO3, record04.REGU_PER_TIL_DTO3, record04.REGU_KOD3);
                adjustments.Add(adj1);
            }

            return adjustments.Count > 0 ? adjustments.OrderBy(x => x.FromDate).ToList() : adjustments;
        }

        private static Adjustment CreateAdjustment(string amount, string sign, string fromDate, string toDate,
    string adjustmentCode)
        {
            return new Adjustment
            {
                Amount = ConversionHelper.GetDecimal100(amount, sign),
                FromDate = ConversionHelper.ParseDate(fromDate),
                ToDate = ConversionHelper.ParseDate(toDate),
                AdjustmentCode = adjustmentCode
            };
        }

        private static ReceiptType GetReceiptType(string adjustmentCode)
        {
            return adjustmentCode == "02" ? ReceiptType.Transfer : ReceiptType.Adjustment;
        }


        private static bool HasValue(string? amountString)
        {
            amountString = amountString?.TrimStart('0');
            return !string.IsNullOrWhiteSpace(amountString);
        }

        private static Type NetsIsSelector(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.Length == 0)
                throw new NullReferenceException();
            var transType = recordLine.Substring(2, 3);
            var recordType = recordLine.Substring(33, 2);

            return transType switch
            {
                "000" => typeof(InfoStartRecord),
                "001" => typeof(InfoSectionStartRecord),
                "005" => InfoRecordDictionary[recordType],
                "009" => typeof(InfoSectionEndRecord),
                "999" => typeof(InfoEndRecord),
                _ => throw new ArgumentOutOfRangeException(transType)
            };
        }

        private static readonly Dictionary<string, Type> InfoRecordDictionary = new Dictionary<string, Type>
        {
            {"00", typeof(InfoRecordFixed00)},
            {"01", typeof(InfoRecordFixed01)},
            {"02", typeof(InfoRecordFixed02)},
            {"03", typeof(InfoRecordFixed03)},
            {"04", typeof(InfoRecordFixed04)},
            {"05", typeof(InfoRecordFixed05)},
            {"10", typeof(InfoRecordFixed10)},
            {"11", typeof(InfoRecordFixed11)},
            {"12", typeof(InfoRecordFixed12)},
            {"13", typeof(InfoRecordFixed13)},
            {"14", typeof(InfoRecordFixed14)},
            {"15", typeof(InfoRecordFixed15)},
            {"16", typeof(InfoRecordFixed16)}
        };

        private static DateTime? GetSubmíssionDate(InfoRecord00 record)
        {
            var date = record.InfoSectionStart.InfoStart.LEV_DTO;
            return string.IsNullOrWhiteSpace(date) ? DateTime.Today : ConversionHelper.ParseDate(date);
        }

    }
}
