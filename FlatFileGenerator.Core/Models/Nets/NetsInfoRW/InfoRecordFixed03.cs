using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed03 : InfoRecordFixedBase
    {
        [FieldFixedLength(8)]
        [FieldOptional]
        public string AFVIGELSE_DTO;

        [FieldFixedLength(2)]
        [FieldOptional]
        public string AFVIGELSE_KOD;

        [FieldFixedLength(12)]
        [FieldOptional]
        public string AFVIGELSES_BLB;

        [FieldFixedLength(1)]
        [FieldOptional]
        public string AFVIGELSES_FRTFLT;

        [FieldFixedLength(5)]
        [FieldOptional]
        public string AFVIGELSES_PCT;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string BESK_GRAD_START_DTO;

        [FieldFixedLength(5)]
        [FieldOptional]
        public string BESK_GRAD_ANT;

        [FieldFixedLength(6)]
        [FieldOptional]
        public string BESK_GRAD_TEL;

        [FieldFixedLength(6)]
        [FieldOptional]
        public string BESK_GRAD_NVN;

        [FieldFixedLength(58)]
        [FieldOptional]
        public string RESERVE1_TXT;
    }
}
