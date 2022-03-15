using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Enums;

namespace LoopBreakers.ReportModule.Models
{
    public class CurrencyStatisticsDTO
    {
        public Currency Currency { get; set; }
        public int Count { get; set; }
    }
}
