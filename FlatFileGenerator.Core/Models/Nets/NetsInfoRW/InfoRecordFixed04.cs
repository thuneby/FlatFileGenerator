using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{

    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed04 : InfoRecordFixedBase
    {
        [FieldFixedLength(2)]
        [FieldOptional]
        public string REGU_KOD1;

        [FieldFixedLength(12)]
        [FieldOptional]
        public string REGU_BLB1;

        [FieldFixedLength(1)]
        [FieldOptional]
        public string REGU_FRTFLT1;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string REGU_PER_FRA_DTO1;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string REGU_PER_TIL_DTO1;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string REGU_KOD2;

        [FieldFixedLength(12)]
        [FieldOptional]
        public string REGU_BLB2;

        [FieldFixedLength(1)]
        [FieldOptional]
        public string REGU_FRTFLT2;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string REGU_PER_FRA_DTO2;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string REGU_PER_TIL_DTO2;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string REGU_KOD3;

        [FieldFixedLength(12)]
        [FieldOptional]
        public string REGU_BLB3;

        [FieldFixedLength(1)]
        [FieldOptional]
        public string REGU_FRTFLT3;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string REGU_PER_FRA_DTO3;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string REGU_PER_TIL_DTO3;

        [FieldFixedLength(18)]
        [FieldOptional]
        public string RESERVE1_TXT;
    }
}
