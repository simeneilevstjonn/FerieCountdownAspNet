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
            return View("Error", new CountdownErrorViewModel
            {
                Message = message
            });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitContactForm()
        {
            if (!IoMaster.VerifyRecaptcha(Request.Form["g-recaptcha-response"], Request.Headers["X-forwarded-for"])) return CustomError("reCAPTCHA validation failed");

            string name = Request.Form["name"];
            string email = Request.Form["email"];
            string subject = Request.Form["subject"];
            string message = Request.Form["message"];

            MailMessage mail = new MailMessage
            {
                Subject = string.Format("FerieCountdown.com contact form: {0}", subject),
                From = new MailAddress(email),
                To = { new MailAddress("simen@feriecountdown.com") },
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                Body = string.Format("<b>Name:</b> {0}<br><b>Email:</b> {1}<br><b>Subject:</b> {2}<br><b>IP:</b> {3}<br><b>IP country:</b> {4}<br><b>Message:</b><br>{5}", name, email, subject, Request.Headers["X-forwarded-for"], Request.Headers["cf-ipcountry"], message)
            };
            IoMaster.SendMail(mail);

            return Redirect("/Home/Contact");
        }
    }
}
