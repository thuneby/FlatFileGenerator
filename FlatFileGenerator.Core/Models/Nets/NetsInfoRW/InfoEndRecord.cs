using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowMoreChars)]
    public class InfoEndRecord : NetsStartEndBase
    {
        [FieldFixedLength(10)]
        public string LEV_TOT_REC_ANT;
        [FieldFixedLength(10)]
        public string LEV_TOT_SECT_ANT;
        [FieldFixedLength(10)]
        public string LEV_TOT_LINIE_ANT;
        [FieldFixedLength(15)]
        public string LEV_TOT_LINIE_BLB;
    }
}
