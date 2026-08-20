namespace FlatFileGenerator.Core.Models
{
    public class Adjustment : GuidModelBase
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Amount { get; set; }
        public string AdjustmentCode { get; set; } = "";
    }
}
