using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Enums;

namespace LoopBreakers.WebApp.DTOs
{
    public class CurrencyStatisticsDTO
    {
        [Display(Name = "currency", ResourceType = typeof(Resources.DTOs.DTOs))]
        public Currency Currency { get; set; }
        [Display(Name = "count", ResourceType = typeof(Resources.DTOs.DTOs))]
        public int Count { get; set; }
    }
}
