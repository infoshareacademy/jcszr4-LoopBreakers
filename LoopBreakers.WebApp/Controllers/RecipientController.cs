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

namespace LoopBreakers.WebApp.Controllers
{
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

        public async Task<ActionResult> Index(SearchRecipientViewModel filter)
        {
            var recipients = await _recipientService.FilterBy(filter);
            var model = _mapper.Map<IEnumerable<RecipientDTO>>(recipients);
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
