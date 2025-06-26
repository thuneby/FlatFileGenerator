namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    public enum IsFixedRecordType
    {
        IsRecordFixed00 = 0,
        IsRecordFixed01 = 1,
        IsRecordFixed02 = 2,
        IsRecordFixed03 = 3,
        IsRecordFixed04 = 4,
        IsRecordFixed05 = 5,
        IsRecordFixed10 = 10,
        IsRecordFixed11 = 11,
        IsRecordFixed12 = 12,
        IsRecordFixed13 = 13,
        IsRecordFixed14 = 14,
        IsRecordFixed15 = 15, 
        IsRecordFixed16 = 16, 
        IsStartRecord = 20,
        SectionStartRecord = 21,
        SectionEndRecord = 22,
        IsEndRecord = 24
    }

    public class NetsIsRecordType 
    {
        public const int Record00 = 0;
        public const int Record01 = 1;
        public const int Record02 = 2;
        public const int Record03 = 3;
        public const int Record04 = 4;
        public const int Record05 = 5;
        public const int Record10 = 10;
        public const int Record11 = 11;
        public const int Record12 = 12;
        public const int Record13 = 13;
        public const int Record14 = 14;
        public const int Record15 = 15;
        public const int Record16 = 16;
        public const int StartRecord = 20;
        public const int SectionStart = 21;
        public const int SectionEnd = 22;
        public const int EndRecord = 24;
    }
}
