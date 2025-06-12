using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoSectionEnd : InfoBase
    {
        [StringLength(10)]
        public string LEV_NR { get; set; }

        [StringLength(8)]
        public string SECT_NR { get; set; }

        [StringLength(10)]
        public string SECT_TOT_REC_ANT { get; set; }

        [StringLength(10)]
        public string SECT_TOT_LINIE_ANT { get; set; }

        [StringLength(15)]
        public string SECT_TOT_LINIE_BLB { get; set; }

        public Guid? InfoSectionStartId { get; set; }
        public InfoSectionStart InfoSectionStart { get; set; }
    }
}
