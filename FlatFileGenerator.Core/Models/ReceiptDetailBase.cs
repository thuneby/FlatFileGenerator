using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatFileGenerator.Core.Models
{
    public abstract class ReceiptDetailBase: GuidModelBase
    {
        [Display(Name = "Periode fra")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Periode til")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ToDate { get; set; }

        [StringLength(10)]
        [Display(Name = "Cpr")]
        public string Cpr { get; set; } = "";

        [Display(Name = "Beløb")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Amount { get; set; }
    }
}
