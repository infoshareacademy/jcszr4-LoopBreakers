using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using LoopBreakers.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IBaseRepository<ApplicationUser> _clientRepository;

        private IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(IBaseRepository<ApplicationUser> clientRepository, IClientService clientrService, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _clientService = clientrService;
            _mapper = mapper;
        }
        // GET: ClientController
        public async Task<ActionResult> Index(SearchClientViewModel user)
        {
            var users = await _clientService.FilterBy(user);
            var model = _mapper.Map<IEnumerable<UserDTO>>(users);
            return View(model);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = _mapper.Map<ApplicationUser>(model);
                user.Created = DateTime.Now;
                user.Registered = DateTime.Now;
                user.IdentityNumber = Guid.NewGuid().ToString();
                await _clientRepository.Create(user);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientController/Edit/5
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

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController/Delete/5
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
