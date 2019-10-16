using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerieCountdown.Classes;
using FerieCountdown.Models;
using Microsoft.AspNetCore.Mvc;

namespace FerieCountdown.Controllers
{
    public class ConfigController : Controller
    {
        public IActionResult Birthday()
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