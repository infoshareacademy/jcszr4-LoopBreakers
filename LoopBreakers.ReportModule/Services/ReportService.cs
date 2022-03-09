using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.ReportModule.Models;

namespace LoopBreakers.ReportModule.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<TransferReport> _transferRepository;
        private readonly IBaseRepository<ActivityReport> _activityRepository;

        public ReportService(IBaseRepository<TransferReport> transferRepository,
                             IBaseRepository<ActivityReport> activityRepository)
        {
            _transferRepository = transferRepository;
            _activityRepository = activityRepository;
        }

        public async Task AddTransferReport(TransferReport transferReport)
        {
            await _transferRepository.Create(transferReport);
        }

        public async Task<IEnumerable<TransferReport>> GetAllTransferReports()
        {
            return await _transferRepository.FindAll();
        }

        public async Task<List<TransferReport>> GetTransferReportByDate(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            return await _transferRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo).ToListAsync();
        }

        public async Task<TransferReport> GetTransferReportById(int id)
        {
            return await _transferRepository.FindById(id);
        }

        public async Task AddActivityReport(ActivityReport activityReport)
        {
            await _activityRepository.Create(activityReport);
        }

        public async Task<IEnumerable<ActivityReport>> GetAllActivityReports()
        {
            return await _activityRepository.FindAll();
        }

        public async Task<ActivityReport> GetActivityReportById(int id)
        {
            return await _activityRepository.FindById(id);
        }

        public async Task<List<ActivityReport>> GetActivityReportByDate(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            return await _activityRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo).ToListAsync();
        }

        public async Task<List<CurrencyStatisticsDTO>> GetCurrencyStatistics()
        {
           return await _transferRepository.GetAllQueryable()
               .GroupBy(s => s.Currency)
               .Select(o => new CurrencyStatisticsDTO()
               {
                   Currency = o.Key,
                   Count = o.Count()
               }).ToListAsync();
        }

        public async Task<List<CurrencyStatisticsDTO>> GetCurrencyStatisticsByDate(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            return await _transferRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo)
                .GroupBy(s => s.Currency)
                .Select(o => new CurrencyStatisticsDTO()
                {
                    Currency = o.Key,
                    Count = o.Count()
                }).ToListAsync();
        }
    }
}
