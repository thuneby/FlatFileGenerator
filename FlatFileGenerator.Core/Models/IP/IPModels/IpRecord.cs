using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models.IP.IPModels
{
    public class IpRecord : IpRecordBase
    {
        [StringLength(8)]
        public string DatoForFratraedelse { get; set; } = "";
    }
}
