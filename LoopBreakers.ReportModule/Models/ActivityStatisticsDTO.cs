using System.ComponentModel.DataAnnotations;
using LoopBreakers.DAL.Enums;

namespace LoopBreakers.ReportModule.Models
{
    public class ActivityStatisticsDTO
    {
        public ActivityEvents ActivityName { get; set; }
        public int Count { get; set; }
    }
}
