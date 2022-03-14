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
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.ReportModule.Models;
using LoopBreakers.WebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace LoopBreakers.WebApp.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class TransferController : Controller
    {
        private readonly ITransferService _transferService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly ReportService _reportService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TransferController(ITransferService transferService, 
                                    IClientService clientService, 
                                    IMapper mapper, 
                                    ReportService reportService,
                                    UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager)
        {
            _transferService = transferService;
            _clientService = clientService;
            _mapper = mapper;
            _reportService = reportService;
            _userManager = userManager;
            _signInManager = signInManager;
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
                var transferRecipient = _clientService.FindRecipient(transfer.Iban);
                transfer.Created= DateTime.Now;
                var transferOut = _mapper.Map<Transfer>(transfer);
                var transferReportOut = _mapper.Map<TransferReportDTO>(transfer);
                transferReportOut.CountryCode = transfer.Iban.Substring(0, 2);

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
                        _clientService.PerformerBalanceUpdateAfterTransfer(currentUser);
                        await _reportService.SendTransferReport(transferReportOut);
                        if (transferRecipient != null)
                        {
                            transferRecipient.Balance = transferRecipient.Balance + transferOut.Amount;
                            _clientService.RecipientBalanceUpdateAfterTransfer(transferRecipient);
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

        public async Task<ActionResult> Index(SearchViewModel filter)
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
