using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Enums;
using LoopBreakers.WebApp.Services;
using LoopBreakers.WebApp.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Hangfire;
using LoopBreakers.WebApp.Helpers;

namespace LoopBreakers.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: ReportController
        public async Task<ActionResult> Index(SearchViewModel filter)
        {
            ReportViewDTO ReportModel = new ReportViewDTO();
            ReportModel.Transfer = await _reportService.GetTransferReport(filter);
            ReportModel.Activity = await _reportService.GetActivityReport(filter);
            ReportModel.Currency = await _reportService.GetCurrencyStatistics(filter);
            ReportModel.LoginCounter = await _reportService.GetLoginStatistics(filter);
            ReportModel.TransferCounter = await _reportService.GetTransferStatistics(filter);
            ReportModel.RegisterCounter = await _reportService.GetRegisterStatistics(filter);
            ReportModel.MostCommonTransferHours = await _reportService.GetMostCommonTransferHoursStatistics(filter);

            if (filter.EmailSend != null)
            {
                BackgroundJobsHelper.LoginActivity = filter.LoginActivity;
                BackgroundJobsHelper.RegisterActivity = filter.RegisterActivity;
                BackgroundJobsHelper.TransferActivity = filter.TransferActivity;
                BackgroundJobsHelper.EmailAddress = filter.EmailAddress;
                RecurringJob.AddOrUpdate(() => _reportService.CallMethodHelperForEmailSending(filter), Cron.Daily(filter.EmailSend.Value.Hour, filter.EmailSend.Value.Minute));
            }

            if (filter.DateTo == null)
            {
                filter.DateTo = DateTime.Now;
            }
            ReportModel.SearchFilter = filter;

            return View(ReportModel);
        }
    }
}
