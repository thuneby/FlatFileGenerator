using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    /// <summary>
    /// Navngivning fra Nets
    /// </summary>
    public class InfoRecord04: InfoRecordChild
    {
        [StringLength(2)]
        public string REGU_KOD1 { get; set; }

        [StringLength(12)]
        public string REGU_BLB1 { get; set; }

        [StringLength(1)]
        public string REGU_FRTFLT1 { get; set; }

        [StringLength(8)]
        public string REGU_PER_FRA_DTO1 { get; set; }

        [StringLength(8)]
        public string REGU_PER_TIL_DTO1 { get; set; }

        [StringLength(2)]
        public string REGU_KOD2 { get; set; }

        [StringLength(12)]
        public string REGU_BLB2 { get; set; }

        [StringLength(1)]
        public string REGU_FRTFLT2 { get; set; }

        [StringLength(8)]
        public string REGU_PER_FRA_DTO2 { get; set; }

        [StringLength(8)]
        public string REGU_PER_TIL_DTO2 { get; set; }

        [StringLength(2)]
        public string REGU_KOD3 { get; set; }

        [StringLength(12)]
        public string REGU_BLB3 { get; set; }

        [StringLength(1)]
        public string REGU_FRTFLT3 { get; set; }

        [StringLength(8)]
        public string REGU_PER_FRA_DTO3 { get; set; }

        [StringLength(8)]
        public string REGU_PER_TIL_DTO3 { get; set; }

        [StringLength(18)]
        public string RESERVE1_TXT { get; set; }
    }
}
