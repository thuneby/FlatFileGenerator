using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed10 : InfoRecordFixedBase
    {
        [FieldFixedLength(3)]
        [FieldOptional]
        public string RESV1_KOD;

        [FieldFixedLength(8)]
        [FieldOptional]
        public string RESV1_TXT;

        [FieldFixedLength(3)]
        [FieldOptional]
        public string RESV2_KOD;

        [FieldFixedLength(10)]
        [FieldOptional]
        public string RESV2_TXT;

        [FieldFixedLength(3)]
        [FieldOptional]
        public string RESV3_KOD;

        [FieldFixedLength(20)]
        [FieldOptional]
        public string RESV3_TXT;

        [FieldFixedLength(3)]
        [FieldOptional]
        public string RESV4_KOD;

        [FieldFixedLength(61)]
        [FieldOptional]
        public string RESV4_TXT;
    }
}
