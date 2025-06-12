using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW

{
    [FixedLengthRecord()]
    public class InfoSectionStartEndBase : NetsBase
    {
        [FieldFixedLength(10)]
        public string LEV_NR;
        [FieldFixedLength(8)]
        public string SECT_NR;

    }
}
