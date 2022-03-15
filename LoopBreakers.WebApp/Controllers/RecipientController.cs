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

namespace LoopBreakers.WebApp.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class RecipientController : Controller
    {
        private readonly IBaseRepository<Recipient> _recipientRepository;
        private readonly IRecipientService _recipientService;
        private readonly IMapper _mapper;
        public RecipientController(IBaseRepository<Recipient> recipientRepository, IRecipientService recipientService, IMapper mapper)
        {
            _recipientRepository = recipientRepository;
            _recipientService = recipientService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(SearchViewModel filter)
        {
            var recipients = await _recipientService.FilterBy(filter);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecipientDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var recipient = _mapper.Map<Recipient>(model);
                recipient.Created = DateTime.Now;
                await _recipientRepository.Create(recipient);

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
