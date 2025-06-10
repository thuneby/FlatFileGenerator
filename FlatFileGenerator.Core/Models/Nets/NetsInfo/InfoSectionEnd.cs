using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoSectionEnd : GuidModelBase
    {

        [StringLength(2)]
        public string SYSTEM_KOD { get; set; }

        [StringLength(3)]
        public string TRANS_TYPE { get; set; }

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
