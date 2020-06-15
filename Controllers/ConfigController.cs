using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerieCountdown.Classes;
using FerieCountdown.Classes.Countdowns;
using FerieCountdown.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using FerieCountdown.Classes.Io;
using FerieCountdown.Classes.Locale;
using FerieCountdown.Classes.TimeHandler;
using System.Globalization;
using Newtonsoft.Json;
using FerieCountdown.Classes.Exceptions;
using FerieCountdownWithAuth;

namespace FerieCountdown.Controllers
{
    [Authorize]
    public class ConfigController : Controller
    {
        private IActionResult CustomError(string message) => View("CustomError", new CountdownErrorViewModel { Message = message });
       
        public IActionResult Custom()
        {
            List<CountdownBackground> cbgs = new List<CountdownBackground>();
            foreach (KeyValuePair<string, CountdownBackground> cb in CountdownBackground.Backgrounds) 
            {
                cbgs.Add(cb.Value);
            }

            return View("Custom", new CustomBuilderViewModel
            {
                Title = "Egendefinert nedtelling",
                Heading = "Lag en nedtelling",
                Action = "CreateCustom",
                BackgroundOptions = cbgs
            });
        }

        [HttpPost]
        public IActionResult CreateCustom()
        {
            //Verify Google reCAPTCHA
            if (!IoMaster.VerifyRecaptcha(Request.Form["g-recaptcha-response"], Request.Headers["X-forwarded-for"], "createcustom")) return CustomError("reCAPTCHA validation failed");

            //Check that form data is provided
            if (string.IsNullOrEmpty(Request.Form["endtext"]) || string.IsNullOrEmpty(Request.Form["cdtext"]) || string.IsNullOrEmpty(Request.Form["background"]) || string.IsNullOrEmpty(Request.Form["time"]) || string.IsNullOrEmpty(Request.Form["recursion"]) || string.IsNullOrEmpty(Request.Form["timezone"])) return View("CustomError", new CountdownErrorViewModel { Message = "Missing one or more required parameters." });

            //Retrieve form data
            string cdtext = Request.Form["cdtext"];
            string endtext = Request.Form["endtext"];
            string type = (string)Request.Form["recursion"] switch
            {
                "yearly" => "custom-reccurring",
                _ => "custom"
            };
            bool uselocal = (string)Request.Form["timezone"] switch
            {
                "local" => true,
                _ => false
            };
            string background = Request.Form["background"];
            DateTime date = DateTime.Parse(Request.Form["time"], null, DateTimeStyles.RoundtripKind);

            CountdownBackground bg = CountdownBackground.Backgrounds[background];

            string countdownid;

            try
            {
                countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), type, date, bg.Path, cdtext, endtext, bg.Html, bg.Css, bg.UseCCC, uselocal);
            }
            catch (BadSqlException)
            {
                //';', '\'', '*', '/', '-', '_', '"'
                return CustomError("Ulovlig input. Tegnene ;, ', *, /, -, _ og \" kan ikke brukes.");
            }

            return Redirect($"/Countdown/Custom/{countdownid}");
        }

        public IActionResult MyCountdowns() => View(new MyCountdownsViewModel { Countdowns = UserCountdownCollections.GetUserCountdowns(User.FindFirstValue(ClaimTypes.NameIdentifier)) });


        public IActionResult DeleteCountdown(string id)
        {
            Startup._DbMaster.SqlQuery($"DELETE from dbo.CustomCountdowns WHERE Id = N'{Startup._DbMaster.ValidateSql(id)}' and Owner = N'{User.FindFirstValue(ClaimTypes.NameIdentifier)}'");

            return Redirect("/Config/MyCountdowns");
        }

        

    }
}