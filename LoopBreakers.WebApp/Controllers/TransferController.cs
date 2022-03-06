using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Enums;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.ReportModule.Models;
using LoopBreakers.WebApp.Services;

namespace LoopBreakers.WebApp.Controllers
{
    public class TransferController : Controller
    {
        private readonly ITransferService _transferService;

        private readonly IClientService _clientService;

        private readonly TransferReportService _reportService;

        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TransferController(ITransferService transferService, IMapper mapper, IClientService clientService,
             UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, TransferReportService reportService)
        {
            _transferService = transferService;
            _clientService = clientService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _reportService = reportService;
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.WrongUser = false;
            ViewBag.NotEnoughMoney = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Create(TransferPerformDTO transfer)
        {
            ViewBag.WrongUser = false;
            ViewBag.NotEnoughMoney = false;
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var userLogon = HttpContext.User.Identity.Name;
                var currentUser = _clientService.FindTransferPerformer(userLogon);
                var transferRecipent = _clientService.FindRecipent(transfer.Iban);
                transfer.Created= DateTime.Now;
                var transferOut = _mapper.Map<Transfer>(transfer);
                var transferReportOut = _mapper.Map<TransferReportDTO>(transfer);

                if(currentUser != null)
                {
                    if (transfer.Amount > currentUser.Balance)
                    {
                        transfer.FromId = currentUser.IdentityNumber;
                        transfer.Currency = (Currency)Enum.Parse(typeof(Currency), currentUser.Currency);
                        transfer.Created = DateTime.Now;
                        ViewBag.NotEnoughMoney = true;
                    }                    
                    else
                    {
                        _transferService.CreateNew(transferOut);
                        currentUser.Balance = currentUser.Balance - transferOut.Amount;
                        _clientService.PerformerBalanceUpadateAfterTransfer(currentUser);
                        if(transferRecipent != null)
                        {
                            transferRecipent.Balance = transferRecipent.Balance + transferOut.Amount;
                            _clientService.RecipentBalanceUpadateAfterTransfer(transferRecipent);
                            await _reportService.SendReport(transferReportOut);
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ViewBag.WrongUser = true;
                }
                return View();

            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Index(SearchTransferViewModel filter)
        {
            var transfers = await _transferService.FilterBy(filter);
            var model = _mapper.Map<IEnumerable<TransferDTO>>(transfers);
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
