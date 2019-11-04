using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace FerieCountdown.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
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
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);
                    await _emailSender.SendEmailAsync(
                         Input.Email,
                         "Vennligst bekreft din e-postadresse",
                         string.Format("<html><body style=\"background-color: #ccc;\"><div style=\"box-shadow:0 2px 5px 0 rgba(0,0,0,0.16),0 2px 10px 0 rgba(0,0,0,0.12);position:absolute;top:50%;left:50%;transform:translate(-50%,-50%);-ms-transform:translate(-50%,-50%);width:50%;height:70%; background-color: white;\"><div style=\"background-color: #333; width:100%; height: 8px\"></div><div style=\"padding:0.01em 16px;height:100%;margin-bottom: 8px;\"> <svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 128 128\" style=\"enable-background:new 0 0 128 128; margin-top: 8px; width: 64px; height: 64px\" xml:space=\"preserve\"><style type=\"text/css\">.st0{fill:#333;stroke:#333;stroke-miterlimit:10}.st1{fill:#CCC}.st2{font - family:'Brandish'}.st3{font - size:83.99px}.st4{font - size:47.2847px}</style><rect x=\"0\" class=\"st0\" width=\"128.2\" height=\"128.2\" rx=\"8\" ry=\"8\"/> <text transform=\"matrix(1.1862 0 0 1 9.2344 86.3721)\" class=\"st1 st2 st3\">F</text> <text transform=\"matrix(1.3357 0 0 1 45.335 105.6272)\" class=\"st1 st2 st4\">CD</text> <rect x=\"14.8\" y=\"86\" class=\"st1\" width=\"16\" height=\"28\"/> </svg><br><div style=\"text-align: center; position: fixed; top: 30%; left:0; right:0; width:100%\"><p> Vennligst bekreft din e-postadresse for å lage din FerieCountdown-bruker.</p> <a href=\"{0}\" style=\"margin-top:2em;border:none;display:inline-block;padding:8px 16px;vertical-align:middle;overflow:hidden;text-decoration:none;color:#ccc;background-color:#333;text-align:center;cursor:pointer;white-space:nowrap\"> Bekreft din e-post </a></div><div style=\"position: fixed; bottom: 2%;right:0;left:0; width:100%;\"><hr><p style=\"padding-left: 1em;padding-right: 1em;\"> Bruker-IP: {1} Land: {2}</p></div></div></div></body></html>", HtmlEncoder.Default.Encode(callbackUrl), Request.Headers["X-forwarded-for"], Request.Headers["cf-ipcountry"]));

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
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
