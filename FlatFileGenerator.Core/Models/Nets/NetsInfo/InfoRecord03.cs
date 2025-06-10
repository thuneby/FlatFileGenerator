using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    /// <summary>
    /// Navngivning fra Nets
    /// </summary>
    public class InfoRecord03: InfoRecordChild
    {
        [StringLength(8)] public string AFVIGELSE_DTO { get; set; }

        [StringLength(2)] public string AFVIGELSE_KOD { get; set; }

        [StringLength(12)] public string AFVIGELSES_BLB { get; set; }

        [StringLength(1)] public string AFVIGELSES_FRTFLT { get; set; }

        [StringLength(5)] public string AFVIGELSES_PCT { get; set; }

        [StringLength(8)] public string BESK_GRAD_START_DTO { get; set; }

        [StringLength(5)] public string BESK_GRAD_ANT { get; set; }

        [StringLength(6)] public string BESK_GRAD_TEL { get; set; }

        [StringLength(6)] public string BESK_GRAD_NVN { get; set; }

        [StringLength(58)] public string RESERVE1_TXT { get; set; }
    }
}
