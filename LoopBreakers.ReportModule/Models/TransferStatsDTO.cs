using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.ReportModule.Models
{
    public class TransferStatsDTO
    {
        [Display(Name = "Total transfers:")]

        public string Name { get; set; }
        public int Count { get; set; }
    }
}
