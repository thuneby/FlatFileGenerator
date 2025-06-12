using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoRecordBase : InfoBase
    {
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
