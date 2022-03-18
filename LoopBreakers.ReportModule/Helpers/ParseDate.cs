using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.ReportModule.Models;

namespace LoopBreakers.ReportModule.Helpers
{
    public static class ParseDate
    {
        public static SearchDate Convert(string dateFrom, string dateTo)
        {
            var result = new SearchDate();
            if (dateFrom != null)
            {
                DateTime.TryParse(dateFrom, out var parsedDateFrom);
                result.dateFrom = parsedDateFrom;
            }
            else
            {
                result.dateFrom = DateTime.MinValue;
            }
            if (dateTo != null)
            {
                DateTime.TryParse(dateTo, out var parsedDateTo);
                result.dateTo = parsedDateTo.AddDays(1);
            }
            else
            {
                result.dateTo = DateTime.Now.AddDays(1);
            }
            return result;
        }
    }
}
