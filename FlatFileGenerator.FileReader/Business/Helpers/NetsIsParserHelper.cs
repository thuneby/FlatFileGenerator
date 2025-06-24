using AutoMapper;
using FlatFileGenerator.Core.Models.Nets.NetsInfo;
using FlatFileGenerator.Core.Models.Nets.NetsInfoRW;
using FlatFileGenerator.FileReader.Business.Mappers;

namespace FlatFileGenerator.FileReader.Business.Helpers
{
    internal class NetsIsParserHelper: ParserHelperBase
    {
        public static IsFixedRecordType GetRecordType(NetsBase record)
        {
            var type = record.GetType();
            if (type == typeof(InfoStartRecord))
                return IsFixedRecordType.IsStartRecord;
            if (type == typeof(InfoSectionStartRecord))
                return IsFixedRecordType.SectionStartRecord;
            if (type == typeof(InfoRecordFixed00))
                return IsFixedRecordType.IsRecordFixed00;
            if (type == typeof(InfoRecordFixed01))
                return IsFixedRecordType.IsRecordFixed01;
            if (type == typeof(InfoRecordFixed02))
                return IsFixedRecordType.IsRecordFixed02;
            if (type == typeof(InfoRecordFixed03))
                return IsFixedRecordType.IsRecordFixed03;
            if (type == typeof(InfoRecordFixed04))
                return IsFixedRecordType.IsRecordFixed04;
            if (type == typeof(InfoRecordFixed05))
                return IsFixedRecordType.IsRecordFixed05;
            if (type == typeof(InfoRecordFixed10))
                return IsFixedRecordType.IsRecordFixed10;
            if (type == typeof(InfoRecordFixed11))
                return IsFixedRecordType.IsRecordFixed11;
            if (type == typeof(InfoRecordFixed12))
                return IsFixedRecordType.IsRecordFixed12;
            if (type == typeof(InfoRecordFixed13))
                return IsFixedRecordType.IsRecordFixed13;
            if (type == typeof(InfoRecordFixed14))
                return IsFixedRecordType.IsRecordFixed14;
            if (type == typeof(InfoRecordFixed15))
                return IsFixedRecordType.IsRecordFixed15;
            if (type == typeof(InfoSectionEndRecord))
                return IsFixedRecordType.SectionEndRecord;
            return type == typeof(InfoEndRecord) ? IsFixedRecordType.IsEndRecord : IsFixedRecordType.IsRecordFixed00;
        }

        public static InfoStart GetInfoStart(object record, InfoStartMapper mapper)
        {
            var textRecord = (InfoStartRecord) record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoSectionStart GetInfoSectionStart(object record, InfoSectionStartMapper mapper)
        {
            var textRecord = (InfoSectionStartRecord) record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoRecord00 GetInfoRecord00(object record, InfoRecord00Mapper mapper)
        {
            var textRecord = (InfoRecordFixed00) record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoRecord01 GetInfoRecord01(object record, InfoRecord01Mapper mapper)
        {
            var textRecord = (InfoRecordFixed01)record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoRecord02 GetInfoRecord02(object record, InfoRecord02Mapper mapper)
        {
            var textRecord = (InfoRecordFixed02)record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoRecord03 GetInfoRecord03(object record, InfoRecord03Mapper mapper)
        {
            var textRecord = (InfoRecordFixed03) record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoRecord04 GetInfoRecord04(object record, InfoRecord04Mapper mapper)
        {
            var textRecord = (InfoRecordFixed04)record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoRecord05 GetInfoRecord05(object record, InfoRecord05Mapper mapper)
        {
            var textRecord = (InfoRecordFixed05)record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoRecord10 GetInfoRecord10(object record, InfoRecord10Mapper mapper)
        {
            var textRecord = (InfoRecordFixed10)record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoSectionEnd GetSectionEnd(object record, InfoSectionEndMapper mapper)
        {
            var textRecord = (InfoSectionEndRecord)record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }

        public static InfoEnd GetInfoEnd(object record, InfoEndMapper mapper)
        {
            var textRecord = (InfoEndRecord)record;
            var result = mapper.GetRecord(textRecord);
            return result;
        }
    }
}
