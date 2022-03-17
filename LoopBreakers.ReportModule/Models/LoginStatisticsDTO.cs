using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.ReportModule.Models
{
    public class LoginStatisticsDTO
    {
        [Display(Name = "Total logins:")]
        public string Name { get; set; }
        public int  Count { get; set; }
    }
}
