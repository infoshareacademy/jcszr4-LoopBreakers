using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoopBreakers.DAL.Entities;
using LoopBreakers.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LoopBreakers.WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ReportService _reportService;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, UserManager<ApplicationUser> userManager, ReportService reportService
)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _reportService = reportService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var currentUser = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            await _reportService.SendActivityReport(new ReportModule.Models.ActivityReportDTO
            {
                Created = DateTime.UtcNow,
                Email = currentUser.Email,
                Description = $"Użytkownik {currentUser.Email} wylogował się",
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName
            });

            if (returnUrl != null)
            {
                return LocalRedirect("/Home/Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
