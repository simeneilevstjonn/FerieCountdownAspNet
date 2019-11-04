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

namespace FerieCountdown.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
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
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Husk meg")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
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

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Bekreftelsese-post sendt. Vennligst sjekk dinn innboks.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                Input.Email,
                "Vennligst bekreft din e-postadresse",
                string.Format("<html><body style=\"background-color: #ccc;\"><div style=\"box-shadow:0 2px 5px 0 rgba(0,0,0,0.16),0 2px 10px 0 rgba(0,0,0,0.12);position:absolute;top:50%;left:50%;transform:translate(-50%,-50%);-ms-transform:translate(-50%,-50%);width:50%;height:70%; background-color: white;\"><div style=\"background-color: #333; width:100%; height: 8px\"></div><div style=\"padding:0.01em 16px;height:100%;margin-bottom: 8px;\"> <svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 128 128\" style=\"enable-background:new 0 0 128 128; margin-top: 8px; width: 64px; height: 64px\" xml:space=\"preserve\"><style type=\"text/css\">.st0{{fill:#333;stroke:#333;stroke-miterlimit:10}}.st1{{fill:#CCC}.st2{{font - family:'Brandish'}}.st3{{font - size:83.99px}.st4{{font - size:47.2847px}}</style><rect x=\"0\" class=\"st0\" width=\"128.2\" height=\"128.2\" rx=\"8\" ry=\"8\"/> <text transform=\"matrix(1.1862 0 0 1 9.2344 86.3721)\" class=\"st1 st2 st3\">F</text> <text transform=\"matrix(1.3357 0 0 1 45.335 105.6272)\" class=\"st1 st2 st4\">CD</text> <rect x=\"14.8\" y=\"86\" class=\"st1\" width=\"16\" height=\"28\"/> </svg><br><div style=\"text-align: center; position: fixed; top: 30%; left:0; right:0; width:100%\"><p> Vennligst bekreft din e-postadresse for å lage din FerieCountdown-bruker.</p> <a href=\"{0}\" style=\"margin-top:2em;border:none;display:inline-block;padding:8px 16px;vertical-align:middle;overflow:hidden;text-decoration:none;color:#ccc;background-color:#333;text-align:center;cursor:pointer;white-space:nowrap\"> Bekreft din e-post </a></div><div style=\"position: fixed; bottom: 2%;right:0;left:0; width:100%;\"><hr><p style=\"padding-left: 1em;padding-right: 1em;\"> Bruker-IP: {1} Land: {2}</p></div></div></div></body></html>", HtmlEncoder.Default.Encode(callbackUrl), Request.Headers["X-forwarded-for"], Request.Headers["cf-ipcountry"]));
            ModelState.AddModelError(string.Empty, "Verifiseringsemail sendt. Vennligst sjekk dinn inboks.");
            return Page();
        }
    }
}
