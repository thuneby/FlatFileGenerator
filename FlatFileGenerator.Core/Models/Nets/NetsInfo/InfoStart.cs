using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoStart : GuidModelBase
    {
        public InfoStart()
        {
            InfoEnd = new HashSet<InfoEnd>();
            InfoSectionStartRecords = new HashSet<InfoSectionStart>();
        }

        [StringLength(2)] public string SYSTEM_KOD { get; set; }

        [StringLength(3)] public string TRANS_TYPE { get; set; }
        [StringLength(3)] public string MODT_PBS_TXT { get; set; }

        [StringLength(10)] public string LEV_NR { get; set; }

        [StringLength(8)] public string LEV_DTO { get; set; }

        [StringLength(8)] public string LEV_SE_NUM { get; set; }

        [StringLength(3)] public string SYSTEM_VERS_NR { get; set; }

        public ICollection<InfoSectionStart> InfoSectionStartRecords { get; set; }
        public ICollection<InfoEnd> InfoEnd { get; set; }
    }
}
