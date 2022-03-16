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
using LoopBreakers.WebApp.Helpers;

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
        public async Task<IActionResult> Create(TransferPerformDTO transfer)
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
                var currentUser = await _clientService.FindTransferPerformer(userLogon);
                var transferRecipient = await _clientService.FindRecipient(transfer.Iban);
                transfer.Created= DateTime.Now;
                var transferOut = _mapper.Map<Transfer>(transfer);
                var transferReportOut = _mapper.Map<TransferReportDTO>(transfer);
                transferReportOut.CountryCode = transfer.Iban.Substring(0, 2);
                await _reportService.SendTransferReport(transferReportOut);

                if (currentUser != null)
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
                        await _transferService.CreateNew(transferOut);
                        currentUser.Balance -= transferOut.Amount;
                        await _clientService.PerformerBalanceUpdateAfterTransfer(currentUser);
                        await _reportService.SendTransferReport(transferReportOut);
                        await _reportService.SendActivityReport(new ActivityReportDTO
                        {
                            Description = $"Użytkownik {currentUser.Email} wykonał przelew",
                            Created = DateTime.Now,
                            Email = currentUser.Email,
                            FirstName = currentUser.FirstName,
                            LastName = currentUser.LastName
                        });

                        if (transferRecipient != null)
                        {
                            transferRecipient.Balance += transferOut.Amount;
                            await _clientService.RecipientBalanceUpdateAfterTransfer(transferRecipient);
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
            if (filter.DateTo == null)
            {
                filter.DateTo = DateTime.Now;
            }
            
            var transfers = await _transferService.FilterBy(filter);
            var transferModel = new TransferViewDTO()
            {
                Transfer = _mapper.Map<IEnumerable<TransferDTO>>(transfers),
                SearchFilter = filter
            };

            return View(transferModel);
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
