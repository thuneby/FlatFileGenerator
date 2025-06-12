using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord()]
    public class InfoRecordFixedBase : InfoSectionStartEndBase
    {
        [FieldFixedLength(10)]
        public string SEKV_NR;
        [FieldFixedLength(2)]
        public string REC_TYP_ID;
    }
}
