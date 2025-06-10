using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models.Nets.NetsInfo
{
    public class InfoRecord01 : InfoRecordChild
    {

        [StringLength(8)]
        public string KUND_START_DATO { get; set; }

        [StringLength(35)]
        public string KUNDE_NAVN { get; set; }

        [StringLength(1)]
        public string OPL_PLIGT_KOD { get; set; }

        [StringLength(2)]
        public string PENS_ALDER_KOD { get; set; }

        [StringLength(2)]
        public string PENS_ALDER { get; set; }

        [StringLength(8)]
        public string AFLOEN_FORM_START_DTO { get; set; }

        [StringLength(2)]
        public string AFLOEN_FORM { get; set; }

        [StringLength(8)]
        public string ANC_FRA_DATO { get; set; }

        [StringLength(8)]
        public string FRATR_DATO { get; set; }

        [StringLength(37)]
        public string RESERVE1_TXT { get; set; }

    }
}
