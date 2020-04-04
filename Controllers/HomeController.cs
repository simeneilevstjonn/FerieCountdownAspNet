using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FerieCountdown.Models;
using System.Net.Mail;
using FerieCountdown.Classes;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using FerieCountdown.Classes.Io;
using System.Security.Claims;

namespace FerieCountdown.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private IActionResult CustomError(string message)
        {
            return View("CustomError", new CountdownErrorViewModel
            {
                Message = message
            });
        }

        public IActionResult Index() => View();

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Copyright()
        {
            return View();
        }

        public IActionResult HowToIncludeSchool()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult HttpError(int id)
        {
            HttpErrorViewModel model = new HttpErrorViewModel { StatusCode = id };
            switch (id)
            {
                case 400:
                    model.ErrorTitle = "Bad Request";
                    model.ErrorDescription = "Your browser sent a request that this server could not understand.";
                    break;
                case 401:
                    model.ErrorTitle = "Unauthorized";
                    model.ErrorDescription = "This server could not verify that you are authorized to access the document requested. Either you supplied the wrong credentials (e.g., bad password), or your browser doesn't understand how to supply the credentials required.";
                    break;
                case 403:
                    model.ErrorTitle = "Forbidden";
                    model.ErrorDescription = "You don't have permission to access this resource.";
                    break;
                case 404:
                    model.ErrorTitle = "Not Found";
                    model.ErrorDescription = "The requested URL was not found on this server.";
                    break;
                case 405:
                    model.ErrorTitle = "Method Not Allowed";
                    model.ErrorDescription = "The target resource does not support the presented method.";
                    break;
                case 500:
                    model.ErrorTitle = "Internal Server Error";
                    model.ErrorDescription = "The server encountered an internal error or misconfiguration and was unable to complete your request.";
                    break;
                default:
                    model.ErrorTitle = "HTTP error " + id;
                    model.ErrorDescription = "HTTP error " + id;
                    break;
            }
            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContactFormAsync()
        {
            if (!IoMaster.VerifyRecaptcha(Request.Form["g-recaptcha-response"], Request.Headers["X-forwarded-for"], "contact")) return CustomError("reCAPTCHA validation failed");

            string name = Request.Form["name"];
            string email = Request.Form["email"];
            string subject = Request.Form["subject"];
            string message = Request.Form["message"];

            EmailSender mailsend = new EmailSender();

            await mailsend.SendEmailAsync(new ContactFormEmail
            {
                Message = message,
                Subject = subject,
                ReplyTo = new MailAddress(email, name),
                UserIP = Request.Headers["X-forwarded-for"],
                UserCountryCode = Request.Headers["cf-ipcountry"]
            });

            TempData["SuccessAlert"] = "Din melding har blitt sendt.";

            return Redirect("/Home/Contact");
        }
    }
}
