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

namespace FerieCountdown.Controllers
{
    public class ConfigController : Controller
    {
        public IActionResult Birthday()
        {
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
                        Type = "date",
                        Description = "Bursdagsdato"
                    }
                },
                BackgroundOptions = new List<CountdownBackground>
                {
                    CountdownBackground.Backgrounds["birthdaycake"]/*,
                    CountdownBackground.Backgrounds["fireworks"]*/
                }
            }); 
        }

        public IActionResult Confirmation()
        {
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
                        Type = "date",
                        Description = "Dato"
                    }
                },
                BackgroundOptions = new List<CountdownBackground>
                {
                    CountdownBackground.Backgrounds["birthdaycake"]/*,
                    CountdownBackground.Backgrounds["fireworks"]*/
                }
            });
        }

        public IActionResult Wedding()
        {
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
                        Type = "date",
                        Description = "Dato"
                    }
                },
                BackgroundOptions = new List<CountdownBackground>
                {
                    CountdownBackground.Backgrounds["birthdaycake"]/*,
                    CountdownBackground.Backgrounds["fireworks"]*/
                }
            });
        }

        [HttpPost]
        public IActionResult CreateBirthday()
        {
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

            //Define identity class

            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), "birthday", date, bg.Path, $"Nedtelling til {nameproperty} bursdag", $"Gratulerer med dagen {name}", bg.Html, bg.Css, bg.UseCCC, true);

            return Redirect($"/Countdown/Custom/{countdownid}");
        }

        [HttpPost]
        public IActionResult CreateConfirmation()
        {
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

            //Define identity class

            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), "confirmation", date, bg.Path, $"Nedtelling til {nameproperty} konfirmasjon", $"{nameproperty} konfirmasjon i dag", bg.Html, bg.Css, bg.UseCCC, true);

            return Redirect($"/Countdown/Custom/{countdownid}");
        }

        [HttpPost]
        public IActionResult CreateWedding()
        {
            //Check that form data is provided
            if (string.IsNullOrEmpty(Request.Form["n0"]) || string.IsNullOrEmpty(Request.Form["n1"]) || string.IsNullOrEmpty(Request.Form["background"]) || string.IsNullOrEmpty(Request.Form["d"])) return View("CustomError", new CountdownErrorViewModel { Message = "Missing one or more required parameters." });

            //Retrieve form data
            string name0 = Request.Form["n0"];
            string name1 = Request.Form["n1"];
            string background = Request.Form["background"];
            DateTime date = DateTime.Parse(Request.Form["d"], null, System.Globalization.DateTimeStyles.RoundtripKind);

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

            //Define identity class

            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), "wedding", date, bg.Path, $"Nedtelling til {name0} og {nameproperty1} bryllup.", $"{name0} og {nameproperty1} bryllup i dag", bg.Html, bg.Css, bg.UseCCC, true);

            return Redirect($"/Countdown/Custom/{countdownid}");
        }
    }
}