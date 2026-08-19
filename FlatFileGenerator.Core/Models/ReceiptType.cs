using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models
{
    public enum ReceiptType
    {
        [Display(Name = "Arbejdsgiverindbetaling")]
        Payment = 0,
        [Display(Name = "Indskud")]
        Transfer = 2,
        [Display(Name = "Efterregulering")]
        Adjustment = 3,
        [Display(Name = "Frivilligt bidrag")]
        Volentary = 4,
        [Display(Name = "Information")]
        Information = 5,
        [Display(Name = "Supplerende bidrag")]
        Supplementory = 6,
        [Display(Name = "Supplerende efterregulering")]
        SupplementoryAdjustment = 7,
        [Display(Name = "Opsparing")]
        Savings = 15,
        [Display(Name = "Paragraf 41 overførsel")]
        P41Transfer = 10,
        [Display(Name = "Ukendt")]
        Unknown = 99,
    }
}
