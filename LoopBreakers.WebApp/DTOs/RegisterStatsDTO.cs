using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.WebApp.DTOs
{
    public class RegisterStatsDTO
    {
        [Display(Name = "New users quantity:")]
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
