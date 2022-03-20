using System.Collections.Generic;
using System.Threading.Tasks;
using LoopBreakers.DAL.Entities;
using LoopBreakers.ReportModule.Models;

namespace LoopBreakers.ReportModule.Services
{
    public interface IReportService
    {
        public Task AddTransferReport (TransferReport transferReport);
        public Task<IEnumerable<TransferReport>> GetAllTransferReports();
        public Task<TransferReport> GetTransferReportById(int id);
        public Task<List<TransferReport>> GetTransferReport(SearchDate filter);
        public Task AddActivityReport (ActivityReport activityReport);
        public Task<IEnumerable<ActivityReport>> GetAllActivityReports();
        public Task<ActivityReport> GetActivityReportById(int id);
        public Task<List<ActivityReport>> GetActivityReport(SearchDate filter);
        public Task<List<CurrencyStatisticsDTO>> GetCurrencyStatistics(SearchDate filter);
        public Task<LoginStatisticsDTO> GetLoginStatistics(SearchDate filter);
        public Task<TransferStatsDTO> GetTransferStatistics(SearchDate filter);
        public Task<RegisterStatsDTO> GetRegisterStatistics(SearchDate filter);
        public Task<List<ActivityStatisticsDTO>> GetActivityStatistics(SearchDate filter);
        public Task<List<MostCommonHoursDTO>> GetTransferStaticsByHours(SearchDate filter);
    }
}
