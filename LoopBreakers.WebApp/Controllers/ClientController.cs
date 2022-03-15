using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.DTOs;
using LoopBreakers.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LoopBreakers.WebApp.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ClientController : Controller
    {
        private readonly IBaseRepository<ApplicationUser> _clientRepository;

        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public ClientController(IBaseRepository<ApplicationUser> clientRepository, IClientService clientService, 
            IMapper mapper, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _clientRepository = clientRepository;
            _clientService = clientService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: ClientController
        public async Task<ActionResult> Index(SearchViewModel filter)
        {
            var users = await _clientService.FilterBy(filter); 
            var model = new ClientViewDTO()
            {
                Client = _mapper.Map<IEnumerable<UserDTO>>(users),
                SearchFilter = filter
            };
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
        public async Task<ActionResult> EditAsync(int id)
        {
            var client = await _clientRepository.FindById(id);
            var model = _mapper.Map<UserDTO>(client);
            return View(model);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, UserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var clientFromDb = _userManager.Users.First(u => u.Id == model.Id);
                var client = _mapper.Map(model, clientFromDb);
                await _clientRepository.Update(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var client = await _clientRepository.FindById(id);
            var model = _mapper.Map<UserDTO>(client);
            return View(model);
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, UserDTO model)
        {
            try
            {
                var client = _mapper.Map<ApplicationUser>(model);
                await _clientRepository.Delete(client);
                return RedirectToAction(nameof(Index)); 
            }
            catch
            {
                return View();
            }
        }
    }
}
