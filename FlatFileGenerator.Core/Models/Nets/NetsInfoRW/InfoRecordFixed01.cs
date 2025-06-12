using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed01: InfoRecordFixedBase
    {
        [FieldOptional]
        [FieldFixedLength(8)]
        public string KUND_START_DATO;

        [FieldOptional]
        [FieldFixedLength(35)]
        public string KUNDE_NAVN;

        [FieldOptional]
        [FieldFixedLength(1)]
        public string OPL_PLIGT_KOD;

        [FieldOptional]
        [FieldFixedLength(2)]
        public string PENS_ALDER_KOD;

        [FieldOptional]
        [FieldFixedLength(2)]
        public string PENS_ALDER;

        [FieldOptional]
        [FieldFixedLength(8)]
        public string AFLOEN_FORM_START_DTO;

        [FieldOptional]
        [FieldFixedLength(2)]
        public string AFLOEN_FORM;

        [FieldOptional]
        [FieldFixedLength(8)]
        public string ANC_FRA_DATO;

        [FieldOptional]
        [FieldFixedLength(8)]
        public string FRATR_DATO;

        [FieldOptional]
        [FieldFixedLength(37)]
        public string RESERVE1_TXT;
    }
}
