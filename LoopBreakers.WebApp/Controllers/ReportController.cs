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
            ReportModel.Transfer = await _reportService.GetTransferReportByDate(filter);
            ReportModel.Activity = await _reportService.GetActivityReportByDate(filter);
            ReportModel.Currency = await _reportService.GetCurrencyStatistics(filter);
            ReportModel.LoginCounter = await _reportService.GetLoginStatistics(filter);
            ReportModel.TransferCounter = await _reportService.GetTransferStatistics(filter);
            ReportModel.RegisterCounter = await _reportService.GetRegisterStatistics(filter);
            ReportModel.MostCommonTransferHours = await _reportService.GetMostCommonTransferHoursStatistics(filter);
            


            
            if (filter.DateTo == null)
            {
                filter.DateTo = DateTime.Now;
            }
            ReportModel.SearchFilter = filter;

            return View(ReportModel);
        }

        // GET: ReportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
