using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;

namespace FlatFileGenerator.FileReader.Business.Helpers
{
    internal class NetsIsParserHelper: ParserHelperBase
    {
        public static Dictionary<Type, int> NetsRecordTypeDictionary = new()
        {
            { typeof(InfoStartRecord), NetsIsRecordType.StartRecord },
            { typeof(InfoSectionStartRecord), NetsIsRecordType.SectionStart },
            { typeof(InfoRecordFixed00), NetsIsRecordType.Record00 },
            { typeof(InfoRecordFixed01), NetsIsRecordType.Record01 },
            { typeof(InfoRecordFixed02), NetsIsRecordType.Record02 },
            { typeof(InfoRecordFixed03), NetsIsRecordType.Record03 },
            { typeof(InfoRecordFixed04), NetsIsRecordType.Record04 },
            { typeof(InfoRecordFixed05), NetsIsRecordType.Record05 },
            { typeof(InfoRecordFixed10), NetsIsRecordType.Record10 },
            { typeof(InfoRecordFixed11), NetsIsRecordType.Record11 },
            { typeof(InfoRecordFixed12), NetsIsRecordType.Record12 },
            { typeof(InfoRecordFixed13), NetsIsRecordType.Record13 },
            { typeof(InfoRecordFixed14), NetsIsRecordType.Record14 },
            { typeof(InfoRecordFixed15), NetsIsRecordType.Record15 },
            { typeof(InfoSectionEndRecord), NetsIsRecordType.SectionEnd },
            { typeof(InfoEndRecord), NetsIsRecordType.EndRecord }
        };

    }
}
