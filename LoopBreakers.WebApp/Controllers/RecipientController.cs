﻿using AutoMapper;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Controllers
{
    public class RecipientController : Controller
    {
        private readonly IRecipientService _recipientService;
        private readonly IMapper _mapper;
        public RecipientController(IRecipientService recipientService, IMapper mapper)
        {
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
