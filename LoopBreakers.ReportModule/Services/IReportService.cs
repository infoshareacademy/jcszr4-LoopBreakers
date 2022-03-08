using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Entities;

namespace LoopBreakers.ReportModule.Services
{
    public interface IReportService
    {
        public Task AddTransferReport (TransferReport transferReport);
        public Task<List<TransferReport>> GetTransferReportByDate(DateTime dateFrom, DateTime dateTo);
        public Task AddActivityReport (ActivityReport activityReport);
    }
}
