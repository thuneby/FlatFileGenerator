using System.ComponentModel.DataAnnotations;

// ReSharper disable InconsistentNaming

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    /// <summary>
    /// Navngivning fra Nets
    /// </summary>
    public class InfoRecord05: InfoRecordChild
    {
        [StringLength(32)]
        public string ADRESSE_1 { get; set; }

        [StringLength(32)]
        public string ADRESSE_2 { get; set; }

        [StringLength(20)]
        public string BY_NAVN { get; set; }

        [StringLength(4)]
        public string POSTNUMMER { get; set; }

        [StringLength(4)]
        public string REG_NUMMER { get; set; }

        [StringLength(10)]
        public string KONTONUMMER { get; set; }

        [StringLength(9)]
        public string RESERVETEKST { get; set; }

    }
}
