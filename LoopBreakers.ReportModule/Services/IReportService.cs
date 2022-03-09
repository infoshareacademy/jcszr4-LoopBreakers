using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Entities;
using LoopBreakers.ReportModule.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace LoopBreakers.ReportModule.Services
{
    public interface IReportService
    {
        public Task AddTransferReport (TransferReport transferReport);
        public Task<IEnumerable<TransferReport>> GetAllTransferReports();
        public Task<TransferReport> GetTransferReportById(int id);
        public Task<List<TransferReport>> GetTransferReportByDate(DateTime dateFrom, DateTime dateTo);
        public Task AddActivityReport (ActivityReport activityReport);
        public Task<IEnumerable<ActivityReport>> GetAllActivityReports();
        public Task<ActivityReport> GetActivityReportById(int id);
        public Task<List<ActivityReport>> GetActivityReportByDate(DateTime dateFrom, DateTime dateTo);
        public Task<List<CurrencyStatisticsDTO>> GetCurrencyStatistics();
        public Task<List<CurrencyStatisticsDTO>> GetCurrencyStatisticsByDate(DateTime dateFrom, DateTime dateTo);
    }
}
