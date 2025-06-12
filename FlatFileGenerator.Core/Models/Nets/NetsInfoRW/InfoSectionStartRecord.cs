using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class InfoSectionStartRecord : InfoSectionStartEndBase
    {
        [FieldFixedLength(8)]
        public string MODT_PBS_NR;
        [FieldFixedLength(10)]
        public string SPEC_NR;
        [FieldFixedLength(8)]
        public string INDBET_SPEC_DTO;
        [FieldFixedLength(8)]
        public string INDBET_DTO;
        [FieldFixedLength(10)]
        public string OPGAVE_NR;
        [FieldFixedLength(3)]
        public string INFOTYPE_KOD;
        [FieldFixedLength(2)]
        public string VENDE_KOD;
        [FieldFixedLength(10)]
        public string OPR_OPGAVE_NR;
        [FieldFixedLength(4)]
        public string MODT_FORDEL_REG_NR;
        [FieldFixedLength(10)]
        public string MODT_FORDEL_KTO_NR;
        [FieldFixedLength(5)]
        public string MODT_FORDEL_PCT;
    }
}
