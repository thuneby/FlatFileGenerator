using FileHelpers;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers.ReceiptDetailMappers;
using FlatFileGenerator.FileReader.Business.Mappers.TextMappers;
using FlatFileGenerator.FileReader.Interfaces;
using System.Collections.Concurrent;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlatFileGenerator.FileReader.Business
{
    public class NetsIsParser: IAsyncParser
    {
        private readonly InfoStartMapper _infoStartMapper = new InfoStartMapper();
        private readonly InfoEndMapper _infoEndMapper = new InfoEndMapper();
        private readonly InfoSectionStartMapper _infoSectionStartMapper = new InfoSectionStartMapper();
        private readonly InfoSectionEndMapper _infoSectionEndMapper = new InfoSectionEndMapper();
        private readonly InfoRecord00Mapper _infoRecord00Mapper = new InfoRecord00Mapper();
        private readonly InfoRecord01Mapper _infoRecord01Mapper = new InfoRecord01Mapper();
        private readonly InfoRecord02Mapper _infoRecord02Mapper = new InfoRecord02Mapper();
        private readonly InfoRecord03Mapper _infoRecord03Mapper = new InfoRecord03Mapper();
        private readonly InfoRecord04Mapper _infoRecord04Mapper = new InfoRecord04Mapper();
        private readonly InfoRecord05Mapper _infoRecord05Mapper = new InfoRecord05Mapper();
        private readonly InfoRecord10Mapper _infoRecord10Mapper = new InfoRecord10Mapper();

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
            var receiptDetailMapper = new InfoRecordMapper();
            Parallel.ForEach(netsModel.InfoSectionStartRecords, startRecord =>
            {
                Parallel.ForEach(startRecord.Record00Records, record =>
                {
                    var receiptDetail = receiptDetailMapper.Map(record);
                    receiptDetail.PaymentDate = ConversionHelper.ParseDate(startRecord.INDBET_DTO);
                    if (receiptDetail != null)
                    {
                        receiptDetails.Add(receiptDetail);
                    }
                });
            });
            if (!string.IsNullOrWhiteSpace(netsModel.LEV_DTO))
            {
                Parallel.ForEach(receiptDetails, receiptDetail =>
                {
                    receiptDetail.SubmissionDate = ConversionHelper.ParseDate(netsModel.LEV_DTO);
                });
            }
            return receiptDetails;
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
