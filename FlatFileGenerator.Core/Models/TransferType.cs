using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models
{
    public enum TransferType
    {
        [Display(Name = "Indberetning")]
        Receipt = 1,
        [Display(Name = "Regulering")]
        Adjustment = 2,
        [Display(Name = "Opkrævning")]
        Invoice = 3,
        [Display(Name = "Overførsel")]
        TransferOfPension = 4,
        [Display(Name = "Opkrævning Ud")]
        InvoiceOut = 5,
        [Display(Name = "Indberetning Ud")]
        ReceiptOut = 6

    }
}
