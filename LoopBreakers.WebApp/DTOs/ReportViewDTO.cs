using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.ReportModule.Models;

namespace LoopBreakers.WebApp.DTOs
{
    public class ReportViewDTO
    {
        public List<TransferReportDTO> Transfer { get; set; }
        public List<ActivityReportDTO> Activity { get; set; }
        public List<CurrencyStatisticsDTO> Currency { get; set; }
        public SearchTransferViewModel SearchFilter { get; set; }
    }
}
