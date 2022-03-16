using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
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
                .Where(s => s.Created >= dateFrom && s.Created < dateTo && (s.Description.Contains("zalogował")|| s.Description.Contains("zalogowal")))
                .ToListAsync();
            return new LoginStatisticsDTO { Name = "Logging quantity: ", Count = data.Count};  
        }
        public async Task<LoginStatisticsDTO> GetAllLoginStatistics()
        {
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.Description.Contains("zalogował") || (s.Description.Contains("zalogowal")))
                .ToListAsync();
            return new LoginStatisticsDTO { Name = "Logging quantity: ", Count = data.Count };
        }
        public async Task<TransferStatsDTO> GetTransferStatistics(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo && s.Description.Contains("przelew"))
                .ToListAsync();
            return new TransferStatsDTO { Name = "Transfers quantity: ", Count = data.Count };
        }
        public async Task<TransferStatsDTO> GetWholeTransferStatistics()
        {
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.Description.Contains("przelew"))
                .ToListAsync();
            return new TransferStatsDTO { Name = "Transfers quantity: ", Count = data.Count };
        }
        public async Task<RegisterStatsDTO> GetRegisterStatistics(DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created < dateTo && (s.Description.Contains("zarejestrował")|| s.Description.Contains("zarejestrował")))
                .ToListAsync();            
            return new RegisterStatsDTO { Name = "Register quantity: ", Count = data.Count };
        }
        public async Task<RegisterStatsDTO> GetWholeRegisterStatistics()
        {
            var data = await _activityRepository.GetAllQueryable()
                .Where(s => (s.Description.Contains("zarejestrował")||s.Description.Contains("zarejestrowal")))
                .ToListAsync();
            return new RegisterStatsDTO { Name = "Register quantity: ", Count = data.Count };
        }
        public async Task<List<MostCommonHoursDTO>> GetTransferStisticsByHours(DateTime dateFrom, DateTime dateTo)
        {
            var dateFromUpdate = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 0, 0, 0, DateTimeKind.Utc);
            var dateTomUpdate = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, 23, 0, 0, DateTimeKind.Utc);
            dateTo = dateTo.AddDays(1).AddHours(11);
            List<MostCommonHoursDTO> mostOverloadHours = new List<MostCommonHoursDTO>();
            mostOverloadHours.Clear();
            for (int i = 0; i <= 23; i++)
            {
                var data = await _activityRepository.GetAllQueryable()
                               .Where(s => s.Created.Hour == i && s.Created >= dateFromUpdate && s.Created < dateTomUpdate && s.Description.Contains("przelew"))
                               .ToListAsync();
                if (data.Count > 0)
                {
                    mostOverloadHours.Add(new MostCommonHoursDTO { Count = data.Count, Hour = data[0].Created.Hour });
                }
                else
                {
                    mostOverloadHours.Add(new MostCommonHoursDTO { Count= 0, Hour = i });
                }
            }
            var sortedData = mostOverloadHours.OrderBy(s=>s.Hour).ToList();
            return sortedData;
            
        }

      
    }
}
