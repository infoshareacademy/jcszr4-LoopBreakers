using System.ComponentModel.DataAnnotations;

namespace LoopBreakers.WebApp.DTOs
{
    public class TransferStatsDTO
    {
        [Display(Name = "Total transfers:")]

        public string Name { get; set; }
        public int Count { get; set; }
    }
}
