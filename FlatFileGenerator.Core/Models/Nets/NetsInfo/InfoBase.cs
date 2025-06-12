using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public abstract class InfoBase: GuidModelBase
    {
        [StringLength(2)] public string SYSTEM_KOD { get; set; }

        [StringLength(3)] public string TRANS_TYPE { get; set; }
    }
}
