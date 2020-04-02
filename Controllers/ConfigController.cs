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

namespace FerieCountdown.Controllers
{
    [Authorize]
    public class ConfigController : Controller
    {
        private IActionResult CustomError(string message)
        {
            return View("CustomError", new CountdownErrorViewModel
            {
                Message = message
            });
        }

        public IActionResult Birthday()
        {
            List<CountdownBackground> cbgs = new List<CountdownBackground>();
            foreach (KeyValuePair<string, CountdownBackground> cb in CountdownBackground.Backgrounds)
            {
                cbgs.Add(cb.Value);
            }
            ViewData["IsPersonalCelebration"] = "true";
            return View("CustomBuilder", new CustomBuilderViewModel
            {
                Title = "Bursdag",
                Heading = "Lag en bursdagsnedtelling",
                Action = "CreateBirthday",
                FormInputs = new List<FormInput>
                {
                    new FormInput
                    {
                        Name = "n",
                        Type = "text",
                        Description = "Navn på person",
                        Placeholder = "Ola"
                    },
                    new FormInput
                    {
                        Name = "d",
                        Type = $"date\" max=\"2038-01-19\" value=\"{@DateTime.UtcNow.ToString("u").Replace(' ', 'T').Substring(0, 9)}",
                        Description = "Bursdagsdato"
                    }
                },
                BackgroundOptions = cbgs
            }); 
        }

        public IActionResult Confirmation()
        {
            List<CountdownBackground> cbgs = new List<CountdownBackground>();
            foreach (KeyValuePair<string, CountdownBackground> cb in CountdownBackground.Backgrounds)
            {
                cbgs.Add(cb.Value);
            }
            ViewData["IsPersonalCelebration"] = "true";
            return View("CustomBuilder", new CustomBuilderViewModel
            {
                Title = "Konfirmasjon",
                Heading = "Lag en konfirmasjonsnedtelling",
                Action = "CreateConfirmation",
                FormInputs = new List<FormInput>
                {
                    new FormInput
                    {
                        Name = "n",
                        Type = "text",
                        Description = "Navn på person",
                        Placeholder = "Ola"
                    },
                    new FormInput
                    {
                        Name = "d",
                        Type = $"date\" min=\"{DateTime.UtcNow.ToString("u").Substring(0, 9)}\" max=\"2038-01-19\" value=\"{@DateTime.UtcNow.ToString("u").Replace(' ', 'T').Substring(0, 9)}",
                        Description = "Dato"
                    }
                },
                BackgroundOptions = cbgs
            });
        }

        public IActionResult Wedding()
        {
            List<CountdownBackground> cbgs = new List<CountdownBackground>();
            foreach (KeyValuePair<string, CountdownBackground> cb in CountdownBackground.Backgrounds)
            {
                cbgs.Add(cb.Value);
            }
            ViewData["IsPersonalCelebration"] = "true";
            return View("CustomBuilder", new CustomBuilderViewModel
            {
                Title = "Bryllup",
                Heading = "Lag en bryllupsnedtelling",
                Action = "CreateWedding",
                FormInputs = new List<FormInput>
                {
                    new FormInput
                    {
                        Name = "n0",
                        Type = "text",
                        Description = "Navn på person 1",
                        Placeholder = "Ola"
                    },
                    new FormInput
                    {
                        Name = "n1",
                        Type = "text",
                        Description = "Navn på person 2",
                        Placeholder = "Kari"
                    },
                    new FormInput
                    {
                        Name = "d",
                        Type = $"date\" min=\"{DateTime.UtcNow.ToString("u").Substring(0, 9)}\" max=\"2038-01-19\" value=\"{@DateTime.UtcNow.ToString("u").Replace(' ', 'T').Substring(0, 9)}",
                        Description = "Dato"
                    }
                },
                BackgroundOptions = cbgs
            });
        }

        public IActionResult Custom()
        {
            List<CountdownBackground> cbgs = new List<CountdownBackground>();
            foreach (KeyValuePair<string, CountdownBackground> cb in CountdownBackground.Backgrounds) 
            {
                cbgs.Add(cb.Value);
            }
            //ViewData["IsPersonalCelebration"] = "true";
            return View("Custom", new CustomBuilderViewModel
            {
                Title = "Egendefinert nedtelling",
                Heading = "Lag en nedtelling",
                Action = "CreateCustom",
                BackgroundOptions = cbgs
            });
        }

        [HttpPost]
        public IActionResult CreateBirthday()
        {
            //Verify Google reCAPTCHA
            if (!IoMaster.VerifyRecaptcha(Request.Form["g-recaptcha-response"], Request.Headers["X-forwarded-for"], "createcelebration")) return CustomError("reCAPTCHA validation failed");

            //Check that form data is provided
            if (string.IsNullOrEmpty(Request.Form["n"]) || string.IsNullOrEmpty(Request.Form["background"]) || string.IsNullOrEmpty(Request.Form["d"])) return View("CustomError", new CountdownErrorViewModel { Message = "Missing one or more required parameters." });

            //Retrieve form data
            string name = Request.Form["n"];
            string background = Request.Form["background"];
            DateTime date = DateTime.Parse(Request.Form["d"], null, System.Globalization.DateTimeStyles.RoundtripKind);

            //Modify name input
            name = name.ToLower();
            name = char.ToUpper(name[0]) + name.Substring(1);
            string nameproperty = name;
            if (name[^1] != 's') nameproperty += 's';

            //Get background
            //TODO implement a way to check if the provided background id is allowed in the selected countdown type
            CountdownBackground bg = CountdownBackground.Backgrounds[background];

            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), "birthday", date, bg.Path, $"Nedtelling til {nameproperty} bursdag", $"Gratulerer med dagen {name}", bg.Html, bg.Css, bg.UseCCC, true);

            return Redirect($"/Countdown/Custom/{countdownid}");
        }

        [HttpPost]
        public IActionResult CreateConfirmation()
        {
            //Verify Google reCAPTCHA
            if (!IoMaster.VerifyRecaptcha(Request.Form["g-recaptcha-response"], Request.Headers["X-forwarded-for"], "createcelebration")) return CustomError("reCAPTCHA validation failed");

            //Check that form data is provided
            if (string.IsNullOrEmpty(Request.Form["n"]) || string.IsNullOrEmpty(Request.Form["background"]) || string.IsNullOrEmpty(Request.Form["d"])) return View("CustomError", new CountdownErrorViewModel { Message = "Missing one or more required parameters." });

            //Retrieve form data
            string name = Request.Form["n"];
            string background = Request.Form["background"];
            DateTime date = DateTime.Parse(Request.Form["d"] + "Z", null, System.Globalization.DateTimeStyles.RoundtripKind);

            //Modify name input
            name = name.ToLower();
            name = char.ToUpper(name[0]) + name.Substring(1);
            string nameproperty = name;
            if (name[^1] != 's') nameproperty += 's';

            //TODO implement a way to check if the provided background id is allowed in the selected countdown type
            CountdownBackground bg = CountdownBackground.Backgrounds[background];

            //Get background

            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), "confirmation", date, bg.Path, $"Nedtelling til {nameproperty} konfirmasjon", $"{nameproperty} konfirmasjon i dag", bg.Html, bg.Css, bg.UseCCC, true);

            return Redirect($"/Countdown/Custom/{countdownid}");
        }

        [HttpPost]
        public IActionResult CreateWedding()
        {
            //Verify Google reCAPTCHA
            if (!IoMaster.VerifyRecaptcha(Request.Form["g-recaptcha-response"], Request.Headers["X-forwarded-for"], "createcelebration")) return CustomError("reCAPTCHA validation failed");

            //Check that form data is provided
            if (string.IsNullOrEmpty(Request.Form["n0"]) || string.IsNullOrEmpty(Request.Form["n1"]) || string.IsNullOrEmpty(Request.Form["background"]) || string.IsNullOrEmpty(Request.Form["d"])) return View("CustomError", new CountdownErrorViewModel { Message = "Missing one or more required parameters." });

            //Retrieve form data
            string name0 = Request.Form["n0"];
            string name1 = Request.Form["n1"];
            string background = Request.Form["background"];
            DateTime date = DateTime.Parse(Request.Form["d"] + "Z", null, System.Globalization.DateTimeStyles.RoundtripKind);

            //Modify name input
            name0 = name0.ToLower();
            name0 = char.ToUpper(name0[0]) + name0.Substring(1);
            string nameproperty0 = name0;
            if (name0[^1] != 's') nameproperty0 += 's';

            name1 = name1.ToLower();
            name1 = char.ToUpper(name1[0]) + name1.Substring(1);
            string nameproperty1 = name1;
            if (name1[^1] != 's') nameproperty1 += 's';

            //Get background
            //TODO implement a way to check if the provided background id is allowed in the selected countdown type
            CountdownBackground bg = CountdownBackground.Backgrounds[background];


            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), "wedding", date, bg.Path, $"Nedtelling til {name0} og {nameproperty1} bryllup.", $"{name0} og {nameproperty1} bryllup i dag", bg.Html, bg.Css, bg.UseCCC, true);

            return Redirect($"/Countdown/Custom/{countdownid}");
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
            DateTime date = DateTime.Parse(Request.Form["time"] + "Z", null, System.Globalization.DateTimeStyles.RoundtripKind);

            //TODO implement a way to check if the provided background id is allowed in the selected countdown type
            CountdownBackground bg = CountdownBackground.Backgrounds[background];

            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), type, date, bg.Path, cdtext, endtext, bg.Html, bg.Css, bg.UseCCC, uselocal);

            return Redirect($"/Countdown/Custom/{countdownid}");
        }

        public IActionResult MyCountdowns()
        {
            return View(new MyCountdownsViewModel { Countdowns = UserCountdownCollections.GetUserCountdowns(User.FindFirstValue(ClaimTypes.NameIdentifier)) });
        }

        public IActionResult DeleteCountdown(string id)
        {
            DbMaster.SqlQuery($"DELETE from dbo.CustomCountdowns WHERE Id = N'{DbMaster.ValidateSql(id)}' and Owner = N'{User.FindFirstValue(ClaimTypes.NameIdentifier)}'");

            return Redirect("/Config/MyCountdowns");
        }

        

    }
}