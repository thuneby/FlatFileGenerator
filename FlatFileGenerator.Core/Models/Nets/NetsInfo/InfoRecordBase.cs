using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoRecordBase : GuidModelBase
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
        public string SEKV_NR { get; set; }

        [StringLength(2)]
        public string REC_TYP_ID { get; set; }
    }
}
