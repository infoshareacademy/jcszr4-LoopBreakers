using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LoopBreakers.DAL.Enums;
using LoopBreakers.ReportModule.Models;
using MoreLinq;

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

        public async Task<LoginStatisticsDTO> GetLoginStatistics(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo && s.ActivityType == ActivityEvents.logging)
                .CountAsync();
            return new LoginStatisticsDTO { Name = "Logging quantity: ", Count = data };  
        }

        public async Task<LoginStatisticsDTO> GetAllLoginStatistics()
        {
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.ActivityType == ActivityEvents.logging)
                .CountAsync();
            return new LoginStatisticsDTO { Name = "Logging quantity: ", Count = data };
        }

        public async Task<TransferStatsDTO> GetTransferStatistics(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo && s.ActivityType == ActivityEvents.transfering)
                .CountAsync();
            return new TransferStatsDTO { Name = "Transfers quantity: ", Count = data };
        }

        public async Task<TransferStatsDTO> GetWholeTransferStatistics()
        {
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.ActivityType == ActivityEvents.transfering)
                .CountAsync();
            return new TransferStatsDTO { Name = "Transfers quantity: ", Count = data };
        }

        public async Task<RegisterStatsDTO> GetRegisterStatistics(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo && s.ActivityType == ActivityEvents.registering)
                .CountAsync();            
            return new RegisterStatsDTO { Name = "Register quantity: ", Count = data };
        }

        public async Task<RegisterStatsDTO> GetWholeRegisterStatistics()
        {
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.ActivityType == ActivityEvents.registering)
                .CountAsync();
            return new RegisterStatsDTO { Name = "Register quantity: ", Count = data };
        }

        public async Task<List<MostCommonHoursDTO>> GetAllTransferStaticsByHours()
        {
            var result = await _transferRepository.GetAllQueryable()
                .GroupBy(g => g.Created.Hour)
                .Select(s => new MostCommonHoursDTO
                {
                    Hour = s.Key,
                    Count = s.Count()
                }).ToListAsync();

            var data = new List<MostCommonHoursDTO>();
            for (int i = 0; i < 24; i++)
            {
                data.Add(new MostCommonHoursDTO(){ Hour = i, Count = 0 });
            }

            foreach (var item in result)
            {
                data[item.Hour].Count = item.Count;
            }

            return data;
        }

        public async Task<List<MostCommonHoursDTO>> GetTransferStaticsByHours(DateTime dateFrom, DateTime dateTo)
        {
            var result = await _transferRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo)
                .GroupBy(g => g.Created.Hour)
                .Select(s => new MostCommonHoursDTO
                {
                    Hour = s.Key,
                    Count = s.Count()
                }).ToListAsync();

            var data = new List<MostCommonHoursDTO>();
            for (int i = 0; i < 24; i++)
            {
                data.Add(new MostCommonHoursDTO() { Hour = i, Count = 0 });
            }

            foreach (var item in result)
            {
                data[item.Hour].Count = item.Count;
            }

            return data;
        }
    }
}
