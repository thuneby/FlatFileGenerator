using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming


namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    /// <summary>
    /// Navngivning fra Nets
    /// </summary>
    public class InfoRecord10: InfoRecordChild
    {
        [StringLength(3)]
        public string RESV1_KOD { get; set; }

        [StringLength(8)]
        public string RESV1_TXT { get; set; }

        [StringLength(3)]
        public string RESV2_KOD { get; set; }

        [StringLength(10)]
        public string RESV2_TXT { get; set; }

        [StringLength(3)]
        public string RESV3_KOD { get; set; }

        [StringLength(20)]
        public string RESV3_TXT { get; set; }

        [StringLength(3)]
        public string RESV4_KOD { get; set; }

        [StringLength(61)]
        public string RESV4_TXT { get; set; }
    }
}
