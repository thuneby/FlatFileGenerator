using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed05 : InfoRecordFixedBase
    {
        [FieldFixedLength(32)]
        [FieldOptional]
        public string ADRESSE_1;

        [FieldFixedLength(32)]
        [FieldOptional]
        public string ADRESSE_2;

        [FieldFixedLength(20)]
        [FieldOptional]
        public string BY_NAVN;

        [FieldFixedLength(4)]
        [FieldOptional]
        public string POSTNUMMER;

        [FieldFixedLength(4)]
        [FieldOptional]
        public string REG_NUMMER;

        [FieldFixedLength(10)]
        [FieldOptional]
        public string KONTONUMMER;

        [FieldFixedLength(9)]
        [FieldOptional]
        public string RESERVETEKST;
    }
}
