using System.ComponentModel.DataAnnotations;
using LoopBreakers.DAL.Enums;

namespace LoopBreakers.WebApp.DTOs
{
    public class ActivityStatisticsDTO
    {
        [Display(Name = "activityName", ResourceType = typeof(Resources.DTOs.DTOs))]
        public ActivityEvents ActivityName { get; set; }
        [Display(Name = "count", ResourceType = typeof(Resources.DTOs.DTOs))]
        public int Count { get; set; }
    }
}
