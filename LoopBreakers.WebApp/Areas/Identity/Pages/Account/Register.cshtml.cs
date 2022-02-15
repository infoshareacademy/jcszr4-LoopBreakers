﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LoopBreakers.DAL.Entities;
using LoopBreakers.DAL.Enums;
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

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public DateTime Created { get; set; } = DateTime.Now;
            //[Required, Display(Name = "ID użytkownika")]
            //[MinLength(25)]
            //[MaxLength(25)]
            public string IdentityNumber { get; set; }
            public bool IsActive { get; set; } = true;
            [Required, Display(Name = "Kwota wejściowa")]
            public decimal Balance { get; set; }
            [Required, Display(Name = "Waluta")]
            public Currency Currency { get; set; }
            [Required, Display(Name = "Wiek"), Range(18,99)]
            public int Age { get; set; }
            [Required, Display(Name = "Imie")]
            public string FirstName { get; set; }
            [Required, Display(Name = "Nazwisko")]
            public string LastName { get; set; }
            [Required, Display(Name = "Płeć")]
            public Gender Gender { get; set; }
            [Display(Name = "Firma")]
            public string Company { get; set; }
            [Required, Display(Name = "Telefon [+48...]"), MinLength(12), MaxLength(13)]
            public string Phone { get; set; }
            [Required, Display(Name = "Adress")]
            public string Address { get; set; }
            public DateTime Registered { get; set; }=DateTime.Now;
            //[Required, Display(Name = "Numer Konta [PL...]"), MinLength(28), MaxLength(28)]
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
                    IdentityNumber = Input.IdentityNumber,
                    IsActive = Input.IsActive,
                    Balance = Input.Balance,
                    Currency = Input.Currency.ToString(),
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
                    _logger.LogInformation("User created a new account with password.");

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
