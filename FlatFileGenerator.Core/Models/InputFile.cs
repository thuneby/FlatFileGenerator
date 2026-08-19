using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models
{
    public class InputFile : GuidModelBase
    {
        [StringLength(255)]
        [Display(Name = "Filnavn")]
        public string FileName { get; set; }
        [Display(Name = "Størrelse i Kb")]
        public decimal Size { get; set; }

        [Display(Name = "Kanal")]
        public DocumentType DocumentType { get; set; }

        public byte[] HashCode { get; set; } = [];
    }
}
