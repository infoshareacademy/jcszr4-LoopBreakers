using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.Data;

namespace LoopBreakers.WebApp.Controllers
{
    public class TransferController : Controller
    {
        // GET: TransferController
        private readonly ITransfersRepository _transfersRepository;
        public TransferController(ITransfersRepository transfersRepository)
        {
            _transfersRepository = transfersRepository;
        }
        public ActionResult Index()
        {
            var model = _transfersRepository.FindAll().Result;
            return View(model);
        }

        // GET: TransferController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransferController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransferController/Create
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

        public ActionResult FilteredByDate()
        {
            
            return View();
        }

        // POST: TransferController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FilteredByDate(DateTime dateFrom, DateTime dateTo)
        {
            //var test = _transfersRepository.FindByDates(dateFrom, dateTo).Result;
            var allTransfer = _transfersRepository.FindAll().Result;
            var model = allTransfer.Where(d=>d.Created>= dateFrom && d.Created <= dateTo).ToList();
            return View("TransfersFilteresByDate", model);
        }

        // GET: TransferController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransferController/Edit/5
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

        // GET: TransferController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransferController/Delete/5
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
