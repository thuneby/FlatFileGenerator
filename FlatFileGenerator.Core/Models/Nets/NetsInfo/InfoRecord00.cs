using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoRecord00 : InfoRecordBase
    {
        public InfoRecord00()
        {
            InfoRecord01 = new HashSet<InfoRecord01>();
            InfoRecord02 = new HashSet<InfoRecord02>();
            InfoRecord03 = new HashSet<InfoRecord03>();
            InfoRecord04 = new HashSet<InfoRecord04>();
            InfoRecord05 = new HashSet<InfoRecord05>();
            InfoRecord10 = new HashSet<InfoRecord10>();
        }

        [StringLength(2)]
        public string REC_ANT { get; set; }

        [StringLength(30)]
        public string LINIE_TRANSID { get; set; }

        [StringLength(8)]
        public string AFS_SE_NR { get; set; }

        [StringLength(8)]
        public string DL_SE_NR { get; set; }

        [StringLength(15)]
        public string KUND_NR_HOS_AFS { get; set; }

        [StringLength(5)]
        public string MODT_AFD_NR { get; set; }

        [StringLength(8)]
        public string AFS_AFT_NR_HOS_MODT { get; set; }

        [StringLength(10)]
        public string KUND_CPR_NR { get; set; }

        [StringLength(15)]
        public string KUND_NR_HOS_MODT { get; set; }

        [StringLength(12)]
        public string INDBET_BLB { get; set; }

        [StringLength(1)]
        public string INDBET_BLB_FRTFLT { get; set; }

        [StringLength(8)]
        public string PERIODE_FRA { get; set; }

        [StringLength(8)]
        public string PERIODE_TIL { get; set; }

        [StringLength(5)]
        public string OVERENSKOMSTNR { get; set; }

        [StringLength(12)]
        public string SPEC_BLB { get; set; }

        [StringLength(2)]
        public string BLANKE { get; set; }

        public ICollection<InfoRecord01> InfoRecord01 { get; set; }
        public ICollection<InfoRecord02> InfoRecord02 { get; set; }
        public ICollection<InfoRecord03> InfoRecord03 { get; set; }
        public ICollection<InfoRecord04> InfoRecord04 { get; set; }
        public ICollection<InfoRecord05> InfoRecord05 { get; set; }
        public ICollection<InfoRecord10> InfoRecord10 { get; set; }

        public Guid? InfoSectionStartId { get; set; }
        public InfoSectionStart InfoSectionStart { get; set; }

    }
}
