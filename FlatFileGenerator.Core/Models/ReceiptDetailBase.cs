using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [StringLength(11)]
        [Display(Name = "Cpr")]
        public string Cpr { get; set; } = "";

        [Display(Name = "Beløb")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Amount { get; set; }
    }
}
