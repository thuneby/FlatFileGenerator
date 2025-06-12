using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class InfoSectionEndRecord : InfoSectionStartEndBase
    {
        [FieldFixedLength(10)]
        public string SECT_TOT_REC_ANT;
        [FieldFixedLength(10)]
        public string SECT_TOT_LINIE_ANT;
        [FieldFixedLength(15)]
        public string SECT_TOT_LINIE_BLB;
    }
}
