using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.ReportModule.Models;

namespace LoopBreakers.WebApp.DTOs
{
    public class ReportViewDTO
    {
        public List<TransferReportDTO> Transfer { get; set; }
        public List<ActivityReportDTO> Activity { get; set; }
        [Display(Name = "firstname", ResourceType = typeof(Resources.DTOs.DTOs))]
        public List<MostCommonHoursDTO> MostCommonTransferHours { get; set; }
        public LoginStatisticsDTO LoginCounter { get; set; }
        public TransferStatsDTO TransferCounter { get; set; }
        public RegisterStatsDTO RegisterCounter { get; set; }
        public List<CurrencyStatisticsDTO> Currency { get; set; }
        public SearchViewModel SearchFilter { get; set; }
    }
}
