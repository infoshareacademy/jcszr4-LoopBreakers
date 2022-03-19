using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Enums;
using LoopBreakers.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace LoopBreakers.WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ReportService _reportService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ReportService reportService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _reportService = reportService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "email", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "password", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "confirmPassword", ResourceType = typeof(Resources.DTOs.DTOs))]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public DateTime Created { get; set; } = DateTime.Now;

            [Required]
            [Display(Name = "balanceForStart", ResourceType = typeof(Resources.DTOs.DTOs))]
            [Range(0, 999999999)]
            public decimal Balance { get; set; }

            [Required]
            [Display(Name = "currency", ResourceType = typeof(Resources.DTOs.DTOs))]
            public Currency Currency { get; set; }
            [Required]
            [Display(Name = "age", ResourceType = typeof(Resources.DTOs.DTOs))]
            [Range(18,120)]
            public int Age { get; set; }
            [Required]
            [Display(Name = "firstname", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string FirstName { get; set; }
            [Required]
            [Display(Name = "lastname", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string LastName { get; set; }
            [Required]
            [Display(Name = "gender", ResourceType = typeof(Resources.DTOs.DTOs))]
            public Gender Gender { get; set; }
            [Display(Name = "company", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string Company { get; set; }
            [Required]
            [Display(Name = "newPhone", ResourceType = typeof(Resources.DTOs.DTOs))]
            [MinLength(12), MaxLength(13)]
            public string Phone { get; set; }
            [Required]
            [Display(Name = "address", ResourceType = typeof(Resources.DTOs.DTOs))]
            public string Address { get; set; }
            public DateTime Registered { get; set; } = DateTime.Now;
            [Required]
            [Display(Name = "newIban", ResourceType = typeof(Resources.DTOs.DTOs))]
            [MinLength(28), MaxLength(28)]
            public string Iban { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,//$"{Input.FirstName}.{Input.LastName}",
                    Email = Input.Email,
                    IdentityNumber = Guid.NewGuid().ToString(),
                    IsActive = true,
                    Balance = Input.Balance,
                    Currency = Input.Currency,
                    Age = Input.Age,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Gender = Input.Gender,
                    Company = Input.Company,
                    Phone = Input.Phone,
                    Registered = DateTime.Now,
                    Created = DateTime.Now,
                    Iban = Input.Iban,
                    Address = Input.Address,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    _logger.LogInformation("User created a new account with password.");
                    await _reportService.SendActivityReport(new ReportModule.Models.ActivityReportDTO
                    {
                        Created = DateTime.UtcNow,
                        Email = user.Email,
                        Description = "User created a new account",
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ActivityType = ActivityEvents.registering
                    });
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
