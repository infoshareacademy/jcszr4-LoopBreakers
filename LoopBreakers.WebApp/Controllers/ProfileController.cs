using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Repositories;
using LoopBreakers.WebApp.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LoopBreakers.WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<ApplicationUser> _clientRepository;

        public ProfileController(UserManager<ApplicationUser> userManager,
                                    IMapper mapper,
                                    IBaseRepository<ApplicationUser> clientRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<ActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return View();
            }
            var modelUser = _mapper.Map<UserDTO>(user);
            return View(modelUser);
        }

        public async Task<ActionResult> Edit()
        {
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity?.Name);
            if (user == null)
            {
                return View();
            }
            var modelUserProfile = _mapper.Map<UserProfileDTO>(user);
            return View(modelUserProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserProfileDTO userProfile)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(HttpContext.User.Identity?.Name);

                if ((user == null) || (!ModelState.IsValid) || (userProfile.Id != user.Id))
                {
                    return View(userProfile);
                }

                var updatedUserProfile = _mapper.Map(userProfile, user);
                await _clientRepository.Update(updatedUserProfile);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
