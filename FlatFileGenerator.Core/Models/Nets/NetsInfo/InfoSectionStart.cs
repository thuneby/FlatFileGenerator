using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoSectionStart : GuidModelBase
    {
        public InfoSectionStart()
    {
        InfoSectionEnd = new HashSet<InfoSectionEnd>();
        Record00Records = new HashSet<InfoRecord00>();
    }

    [StringLength(2)]
    public string SYSTEM_KOD { get; set; }

    [StringLength(3)]
    public string TRANS_TYPE { get; set; }

    [StringLength(10)]
    public string LEV_NR { get; set; }

    [StringLength(8)]
    public string SECT_NR { get; set; }

    [StringLength(8)]
    public string MODT_PBS_NR { get; set; }

    [StringLength(10)]
    public string SPEC_NR { get; set; }

    [StringLength(8)]
    public string INDBET_SPEC_DTO { get; set; }

    [StringLength(8)]
    public string INDBET_DTO { get; set; }

    [StringLength(10)]
    public string OPGAVE_NR { get; set; }

    [StringLength(3)]
    public string INFOTYPE_KOD { get; set; }

    [StringLength(2)]
    public string VENDE_KOD { get; set; }

    [StringLength(10)]
    public string OPR_OPGAVE_NR { get; set; }

    [StringLength(4)]
    public string MODT_FORDEL_REG_NR { get; set; }

    [StringLength(10)]
    public string MODT_FORDEL_KTO_NR { get; set; }

    [StringLength(5)]
    public string MODT_FORDEL_PCT { get; set; }

    public ICollection<InfoRecord00> Record00Records { get; set; }
    public ICollection<InfoSectionEnd> InfoSectionEnd { get; set; }

    public Guid? InfoStartId { get; set; }

    public InfoStart InfoStart { get; set; }
}
}
