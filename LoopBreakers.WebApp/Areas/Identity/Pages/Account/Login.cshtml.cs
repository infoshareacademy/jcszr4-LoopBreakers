using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Enums;
using LoopBreakers.WebApp.DTOs;
using LoopBreakers.WebApp.Services;

namespace LoopBreakers.WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ReportService _reportService;
        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            ReportService reportService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _reportService = reportService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "email", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "password", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string Password { get; set; }

            [Display(Name = "rememberMe", ResourceType = typeof(Resources.DTOs.DTOs))]
            public bool RememberMe { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();            
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        
            if (ModelState.IsValid)
            {

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    
                    _logger.LogInformation("User logged in.");
                    var currentUser = await _userManager.FindByEmailAsync(Input.Email);
                    await _reportService.SendActivityReport(new ActivityReportDTO 
                    { 
                        Created = DateTime.UtcNow,
                        Email = Input.Email,
                        Description = "User login to app",
                        FirstName = currentUser.FirstName,
                        LastName = currentUser.LastName,
                        ActivityType = ActivityEvents.logging
                    });

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
