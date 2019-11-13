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
            return View("CustomError", new CountdownErrorViewModel
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

        public IActionResult Copyright()
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
            if (!IoMaster.VerifyRecaptcha(Request.Form["g-recaptcha-response"], Request.Headers["X-forwarded-for"])) return CustomError("reCAPTCHA validation failed");

            string name = Request.Form["name"];
            string email = Request.Form["email"];
            string subject = Request.Form["subject"];
            string message = Request.Form["message"];

            EmailSender mailsend = new EmailSender();
            await mailsend.SendEmailAsync("simen@feriecountdown.com", string.Format("FerieCountdown.com contact form: {0}", subject), string.Format("<html><body style=\"background-color: #ccc;\"><div style=\"box-shadow:0 2px 5px 0 rgba(0,0,0,0.16),0 2px 10px 0 rgba(0,0,0,0.12);position:absolute;top:50%;left:50%;transform:translate(-50%,-50%);-ms-transform:translate(-50%,-50%);width:50%;height:70%; background-color: white;\"><div style=\"background-color: #333; width:100%; height: 8px\"></div><div style=\"padding:0.01em 16px;height:100%;margin-bottom: 8px;\"> <svg version=\"1.1\" id=\"Layer_1\" xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" x=\"0px\" y=\"0px\" viewBox=\"0 0 128 128\" style=\"enable-background:new 0 0 128 128; margin-top: 8px; width: 64px; height: 64px\" xml:space=\"preserve\"><style type=\"text/css\">.st0{{fill:#333;stroke:#333;stroke-miterlimit:10}}.st1{{fill:#CCC}}.st2{{font-family:'Brandish'}}.st3{{font-size:83.99px}}.st4{{font-size:47.2847px}}</style><rect x=\"0\" class=\"st0\" width=\"128.2\" height=\"128.2\" rx=\"8\" ry=\"8\"/> <text transform=\"matrix(1.1862 0 0 1 9.2344 86.3721)\" class=\"st1 st2 st3\">F</text> <text transform=\"matrix(1.3357 0 0 1 45.335 105.6272)\" class=\"st1 st2 st4\">CD</text> <rect x=\"14.8\" y=\"86\" class=\"st1\" width=\"16\" height=\"28\"/> </svg><br><div style=\"text-align: center; position: fixed; top: 30%; left:0; right:0; width:100%\"><h3>{0}</h3><p> Fra: {1} &lt;{2}&gt;<br><br>{3}</p></div><div style=\"position: fixed; bottom: 2%;right:0;left:0; width:100%;\"><hr><p style=\"padding-left: 1em;padding-right: 1em;\"> Bruker-IP: {4} Land: {5}</p></div></div></div></body></html>", subject, name, email, message, Request.Headers["X-forwarded-for"], Request.Headers["cf-ipcountry"]));

            return Redirect("/Home/Contact");
        }
    }
}
