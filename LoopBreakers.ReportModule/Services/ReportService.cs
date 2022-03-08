using LoopBreakers.DAL.Context;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<List<TransferReport>> GetTransferReportByDate(DateTime dateFrom, DateTime dateTo)
        {
            return await _transferRepository.GetAllQueryable()
                .Where(s => s.Created >= dateFrom && s.Created <= dateTo.AddDays(1).AddSeconds(-1)).ToListAsync();
        }

        public async Task AddTransferReport(TransferReport transferReport)
        {
            await _transferRepository.Create(transferReport);
        }

        public async Task<IEnumerable<TransferReport>> GetAllTransferReports()
        {
            return await _transferRepository.FindAll();
        }
        public async Task AddActivityReport(ActivityReport activityReport)
        {
            await _activityRepository.Create(activityReport);
        }
    }
}
