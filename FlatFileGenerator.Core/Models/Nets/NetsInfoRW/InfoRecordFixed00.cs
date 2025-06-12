using FileHelpers;
// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    [FixedLengthRecord(FixedMode.AllowVariableLength)]
    public class InfoRecordFixed00 : InfoRecordFixedBase
    {
        [FieldFixedLength(2)]
        public string REC_ANT;
        [FieldFixedLength(30)]
        public string LINIE_TRANSID;
        [FieldFixedLength(8)]
        public string AFS_SE_NR;
        [FieldFixedLength(8)]
        public string DL_SE_NR;
        [FieldFixedLength(15)]
        public string KUND_NR_HOS_AFS;
        [FieldFixedLength(5)]
        public string MODT_AFD_NR;
        [FieldFixedLength(8)]
        public string AFS_AFT_NR_HOS_MODT;
        [FieldFixedLength(10)]
        public string KUND_CPR_NR;
        [FieldFixedLength(15)]
        public string KUND_NR_HOS_MODT;
        [FieldFixedLength(12)]
        public string INDBET_BLB;
        [FieldFixedLength(1)]
        public string INDBET_BLB_FRTFLT;
        [FieldFixedLength(8)]
        public string PERIODE_FRA;
        [FieldFixedLength(8)]
        public string PERIODE_TIL;
        [FieldOptional]
        [FieldFixedLength(5)]
        public string OVERENSKOMSTNR;
        [FieldFixedLength(12)]
        [FieldOptional]
        public string SPEC_BLB;
        [FieldFixedLength(2)]
        [FieldOptional]
        public string BLANKE;
    }
}
