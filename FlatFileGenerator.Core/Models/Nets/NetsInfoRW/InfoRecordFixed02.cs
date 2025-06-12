using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed02 : InfoRecordFixedBase
    {
        [FieldOptional]
        [FieldFixedLength(8)]
        public string PENS_GIV_LOEN_START_DTO;
        [FieldOptional]
        [FieldFixedLength(12)]
        public string PENS_GIV_LOEN_BLB;
        [FieldOptional]
        [FieldFixedLength(8)]
        public string PENS_TYPE_START_DTO;
        [FieldOptional]
        [FieldFixedLength(2)]
        public string PENS_TYPE;
        [FieldOptional]
        [FieldFixedLength(8)]
        public string NORM_BIDRAG_START_DTO;
        [FieldOptional]
        [FieldFixedLength(12)]
        public string NORM_BIDRAG_BLB;
        [FieldOptional]
        [FieldFixedLength(12)]
        public string ARB_ANDEL_BIDRAG_BLB;
        [FieldOptional]
        [FieldFixedLength(8)]
        public string PENS_BIDRAG_PCT_START_DTO;
        [FieldOptional]
        [FieldFixedLength(4)]
        public string PENS_BIDRAG_PCT;
        [FieldOptional]
        [FieldFixedLength(4)]
        public string ARB_ANDEL_PENS_BIDRAG_PCT;
        [FieldOptional]
        [FieldFixedLength(8)]
        public string LOEN_TRIN_START_DTO;
        [FieldOptional]
        [FieldFixedLength(5)]
        public string LOEN_TRIN_NR;
        [FieldOptional]
        [FieldFixedLength(8)]
        public string GRP_ANDEL_START_DTO;
        [FieldOptional]
        [FieldFixedLength(12)]
        public string GRP_ANDEL_BLB;

    }
}
