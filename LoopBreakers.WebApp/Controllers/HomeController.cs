using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.Contracts;
using LoopBreakers.WebApp.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.WebApp.DTOs;
using LoopBreakers.DAL.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account;

namespace LoopBreakers.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IBaseRepository<Transfer> _transferRepository;
        private readonly ITransferService _transferService;
        private readonly IMapper _mapper;
        
        public HomeController(ILogger<HomeController> logger, 
                                UserManager<ApplicationUser> userManager, 
                                SignInManager<ApplicationUser> signInManager,
                                IBaseRepository<Transfer> transferRepository,
                                ITransferService transferService,
                                IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _transferRepository = transferRepository;
            _transferService = transferService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(30) });

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    var user = await _userManager.FindByNameAsync(HttpContext?.User?.Identity?.Name);
                    var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    if (isAdmin)
                        return RedirectToAction("", "client");

                    var homeView = new HomePageViewDTO();
                    var filter = new SearchViewModel()
                    {
                        DateFrom = DateTime.Now.AddYears(-50),
                        DateTo = null
                    };
                    var transfers = await _transferService.FilterBy(filter, user);
                    var transfersDto = _mapper.Map<IEnumerable<TransferDTO>>(transfers);
                    transfersDto = transfersDto.Take(10);
                    homeView.AccountNumber = user.Iban;
                    homeView.Firstname = user.FirstName;
                    homeView.Lastname = user.LastName;
                    homeView.Balance = user.Balance;
                    homeView.TransfersHistory = transfersDto;
                    homeView.Currency = user.Currency;

                    return View(homeView);
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
    }
}
