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

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

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


        [Route("Transfer/{dateFrom}")]
        public ActionResult Index(DateTime dateFrom, DateTime dateTo)
        {
            var model = _transfersRepository.FindByDates(dateFrom, dateTo).Result;
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

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

        public ActionResult Delete(int id)
        {
            return View();
        }

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
