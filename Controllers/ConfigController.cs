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
        /*public IActionResult Birthday()
        {
            ViewData["IsPersonalCelebration"] = "true";
            return View("CountdownConfigurator", new CountdownConfiguratorViewModel
            {
                Title = "Bursdag",
                Heading = "Lag en bursdagsnedtelling",
                Controller = "Countdown",
                Action = "Birthday",
                Inputs = new List<FormInput> { 
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
                }
            });
        }*/

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
                    CountdownBackground.Backgrounds["birthdaycake"],
                    CountdownBackground.Backgrounds["fireworks"]
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
            if (name[name.Length - 1] != 's') nameproperty += 's';

            //Get background
            //TODO implement a way to check if the provided background id is allowed in the selected countdown type
            CountdownBackground bg = CountdownBackground.Backgrounds[background];

            //Define identity class

            string countdownid = CountdownSqlAgent.CreateCustomCountdown(User.FindFirstValue(ClaimTypes.NameIdentifier), "birthday", date, bg.Path, $"Nedtelling til {nameproperty} bursdag", $"Gratulerer med dagen {name}", bg.Html, bg.Css, true, true);

            return Redirect($"/Countdown/Custom/{countdownid}");
        }

        public IActionResult Confirmation()
        {
            ViewData["IsPersonalCelebration"] = "true";
            return View("CountdownConfigurator", new CountdownConfiguratorViewModel
            {
                Title = "Konfirmasjon",
                Heading = "Lag en konfirmasjonsnedtelling",
                Controller = "Countdown",
                Action = "Confirmation",
                Inputs = new List<FormInput> {
                    new FormInput
                    {
                        Name = "n",
                        Type = "text",
                        Description = "Navn på konfirmant",
                        Placeholder = "Ola"
                    },
                    new FormInput
                    {
                        Name = "d",
                        Type = "date",
                        Description = "Dato"
                    }
                }
            });
        }

        public IActionResult Wedding()
        {
            ViewData["IsPersonalCelebration"] = "true";
            return View("CountdownConfigurator", new CountdownConfiguratorViewModel
            {
                Title = "Bryllup",
                Heading = "Lag en bryllupsnedtelling",
                Controller = "Countdown",
                Action = "Wedding",
                Inputs = new List<FormInput> {
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
                }
            });
        }
    }
}