using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LoopBreakers.WebApp.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class RecipientController : Controller
    {
        private readonly IBaseRepository<Recipient> _recipientRepository;
        private readonly IRecipientService _recipientService;
        private readonly ITransferService _transferService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public RecipientController(IBaseRepository<Recipient> recipientRepository, 
                                    IRecipientService recipientService,
                                    ITransferService transferService,
                                    IMapper mapper, 
                                    UserManager<ApplicationUser> userManager)
        {
            _recipientRepository = recipientRepository;
            _recipientService = recipientService;
            _mapper = mapper;
            _userManager = userManager;
            _transferService = transferService;
        }

        public async Task<ActionResult> Index(SearchViewModel filter)
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            var recipients = await _recipientService.FilterBy(filter, user);
            var recipientsModel = new RecipientViewDTO()
            {
                Recipient = _mapper.Map<IEnumerable<RecipientDTO>>(recipients),
                SearchFilter = filter
            };
            return View(recipientsModel);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.WrongUser = false; 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecipientDTO model)
        {
            ViewBag.WrongUser = false; 
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (user.Iban == model.Iban)
                {
                    ViewBag.WrongUser = true;
                    return View();
                }
                var recipient = _mapper.Map<Recipient>(model);
                recipient.Created = DateTime.Now;
                recipient.FromId = user.Id.ToString();
                await _recipientRepository.Create(recipient);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Send(int id)
        {
            ViewBag.NotEnoughMoney = false;
            ViewBag.WrongUser = false;
            var recipient = await _recipientRepository.FindById(id);
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (recipient.FromId != user.Id.ToString())
            {
                return RedirectToAction(nameof(Index));
            }
            var modelReceiver = _mapper.Map<RecipientDTO>(recipient);
            var modelTransfer = _mapper.Map<TransferPerformDTO>(modelReceiver);

            return View(modelTransfer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendAsync(int id, TransferPerformDTO transfer)
        {
            ViewBag.NotEnoughMoney = false;
            ViewBag.WrongUser = false;
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                if (user.Iban == transfer.Iban)
                {
                    ViewBag.WrongUser = true;
                    return View();
                }
                
                var result = await _transferService.SendTransfer(transfer, user);
                if (!result)
                {
                    ViewBag.NotEnoughMoney = true;
                    return View();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var recipient = await _recipientRepository.FindById(id);
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (recipient.FromId != user.Id.ToString())
            {
                return RedirectToAction(nameof(Index));
            }
            var model = _mapper.Map<RecipientDTO>(recipient);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, RecipientDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var recipient = _mapper.Map<Recipient>(model);
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
                recipient.FromId = user.Id.ToString();
                await _recipientRepository.Update(recipient);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            var recipient = await _recipientRepository.FindById(id);
            var model = _mapper.Map<RecipientDTO>(recipient);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, RecipientDTO model)
        {
            try
            {
                var recipient = _mapper.Map<Recipient>(model);
                await _recipientRepository.Delete(recipient);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
