using FileHelpers;
using FlatFileGenerator.Core.Models;
using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using FlatFileGenerator.FileReader.Business.Helpers;
using FlatFileGenerator.FileReader.Business.Mappers;
using FlatFileGenerator.FileReader.Interfaces;

namespace FlatFileGenerator.FileReader.Business
{
    public class NetsIsParser: IAsyncParser
    {
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
                var exception = new Exception("Fejl - Formatet er ikke Nets IS!");
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
                var type = NetsIsParserHelper.GetRecordType((NetsBase)record);
                switch (type)
                {
                    case IsFixedRecordType.IsStartRecord:
                        netsModel = NetsIsParserHelper.GetInfoStart(record, _infoStartMapper);
                        break;
                    case IsFixedRecordType.SectionStartRecord:
                        var sectionStart = NetsIsParserHelper.GetInfoSectionStart(record, _infoSectionStartMapper);
                        sectionStart.InfoStartId = netsModel.Id;
                        netsModel.InfoSectionStartRecords.Add(sectionStart);
                        currentSection = sectionStart;
                        break;
                    case IsFixedRecordType.IsRecordFixed00:
                        var infoRecord = NetsIsParserHelper.GetInfoRecord00(record, _infoRecord00Mapper);
                        infoRecord.InfoSectionStartId = currentSection.Id;
                        currentRecord00 = infoRecord;
                        currentSection.Record00Records.Add(currentRecord00);
                        break;
                    case IsFixedRecordType.IsRecordFixed01:
                        var record01 = NetsIsParserHelper.GetInfoRecord01(record, _infoRecord01Mapper);
                        record01.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord01.Add(record01);
                        break;
                    case IsFixedRecordType.IsRecordFixed02:
                        var record02 = NetsIsParserHelper.GetInfoRecord02(record, _infoRecord02Mapper);
                        record02.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord02.Add(record02);
                        break;
                    case IsFixedRecordType.IsRecordFixed03:
                        var record03 = NetsIsParserHelper.GetInfoRecord03(record, _infoRecord03Mapper);
                        record03.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord03.Add(record03);
                        break;
                    case IsFixedRecordType.IsRecordFixed04:
                        var record04 = NetsIsParserHelper.GetInfoRecord04(record, _infoRecord04Mapper);
                        record04.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord04.Add(record04);
                        break;
                    case IsFixedRecordType.IsRecordFixed05:
                        var record05 = NetsIsParserHelper.GetInfoRecord05(record, _infoRecord05Mapper);
                        record05.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord05.Add(record05);
                        break;
                    case IsFixedRecordType.IsRecordFixed10:
                        var record10 = NetsIsParserHelper.GetInfoRecord10(record, _infoRecord10Mapper);
                        record10.InfoRecord00Id = currentRecord00.Id;
                        currentRecord00.InfoRecord10.Add(record10);
                        break;
                    case IsFixedRecordType.IsRecordFixed11:
                    case IsFixedRecordType.IsRecordFixed12:
                    case IsFixedRecordType.IsRecordFixed13:
                    case IsFixedRecordType.IsRecordFixed14:
                    case IsFixedRecordType.IsRecordFixed15:
                    case IsFixedRecordType.IsRecordFixed16:
                        break;
                    case IsFixedRecordType.SectionEndRecord:
                        var sectionEnd = NetsIsParserHelper.GetSectionEnd(record, _infoSectionEndMapper);
                        sectionEnd.InfoSectionStartId = currentSection.Id;
                        currentSection.InfoSectionEnd.Add(sectionEnd);
                        break;
                    case IsFixedRecordType.IsEndRecord:
                        var infoend = NetsIsParserHelper.GetInfoEnd(record, _infoEndMapper);
                        infoend.InfoStartId = netsModel.Id;
                        netsModel.InfoEnd.Add(infoend);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var receiptDetails = new HashSet<ReceiptDetail>();
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
    }
}
