using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    public class NetsStartEndBase: NetsBase
    {
        [FieldFixedLength(3)]
        public string MODT_PBS_TXT ;
        [FieldFixedLength(10)]
        public string LEV_NR;
        [FieldFixedLength(8)]
        public string LEV_DTO;
        [FieldFixedLength(8)]
        public string LEV_SE_NUM;
        [FieldFixedLength(3)]
        public string SYSTEM_VERS_NR;
    }
}
