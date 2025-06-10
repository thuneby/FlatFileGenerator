using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    /// <summary>
    /// Navngivning fra Nets
    /// </summary>
    public class InfoRecord02 : InfoRecordChild
    {
        [StringLength(8)]
        public string PENS_GIV_LOEN_START_DTO{ get; set; }

        [StringLength(12)]
        public string PENS_GIV_LOEN_BLB{ get; set; }

        [StringLength(8)]
        public string PENS_TYPE_START_DTO{ get; set; }

        [StringLength(2)]
        public string PENS_TYPE{ get; set; }

        [StringLength(8)]
        public string NORM_BIDRAG_START_DTO{ get; set; }

        [StringLength(12)]
        public string NORM_BIDRAG_BLB{ get; set; }

        [StringLength(12)]
        public string ARB_ANDEL_BIDRAG_BLB{ get; set; }

        [StringLength(8)]
        public string PENS_BIDRAG_PCT_START_DTO{ get; set; }

        [StringLength(4)]
        public string PENS_BIDRAG_PCT{ get; set; }

        [StringLength(4)]
        public string ARB_ANDEL_PENS_BIDRAG_PCT{ get; set; }

        [StringLength(8)]
        public string LOEN_TRIN_START_DTO{ get; set; }

        [StringLength(5)]
        public string LOEN_TRIN_NR{ get; set; }

        [StringLength(8)]
        public string GRP_ANDEL_START_DTO{ get; set; }

        [StringLength(12)]
        public string GRP_ANDEL_BLB{ get; set; }
    }
}
