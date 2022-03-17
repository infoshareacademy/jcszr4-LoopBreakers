using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.ReportModule.Models
{
    public class RegisterStatsDTO
    {
        [Display(Name = "New users quantity:")]
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
