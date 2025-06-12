using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfoRW
{
    public class NetsBase
    {
        [StringLength(2)]
        public string SYSTEM_KOD { get; set; }

        [StringLength(3)]
        public string TRANS_TYPE { get; set; }
    }
}