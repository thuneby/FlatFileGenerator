using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoEnd : InfoBase
    {
        [StringLength(3)]
        public string MODT_PBS_TXT { get; set; }

        [StringLength(10)]
        public string LEV_NR { get; set; }

        [StringLength(8)]
        public string LEV_DTO { get; set; }

        [StringLength(8)]
        public string LEV_SE_NUM { get; set; }

        [StringLength(3)]
        public string SYSTEM_VERS_NR { get; set; }

        [StringLength(10)]
        public string LEV_TOT_REC_ANT { get; set; }

        [StringLength(10)]
        public string LEV_TOT_SECT_ANT { get; set; }

        [StringLength(10)]
        public string LEV_TOT_LINIE_ANT { get; set; }

        [StringLength(15)]
        public string LEV_TOT_LINIE_BLB { get; set; }

        public Guid? InfoStartId { get; set; }
        public InfoStart InfoStart { get; set; }
    }
}
