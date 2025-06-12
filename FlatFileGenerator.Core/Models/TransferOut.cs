using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlatFileGenerator.Core.Models
{
    public class TransferOut : GuidModelBase
    {
        [Display(Name = "Bankdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BankTrxDate { get; set; }

        [Display(Name = "Valørdato")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime BankValDate { get; set; }

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
        [Display(Name = "Police")]
        public int PolicyNumber { get; set; }
        [Display(Name = "Aftale")]
        public int AgreementNumber { get; set; }
        public string Payload { get; set; } = "";

        [Display(Name = "Type")]
        public TransferType TransferType { get; set; }
    }
}
