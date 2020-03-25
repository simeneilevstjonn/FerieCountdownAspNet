using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerieCountdown.Models;
using Microsoft.AspNetCore.Mvc;

namespace FerieCountdown.Controllers
{
    public class SetLocaleController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Work() => View("CustomLocaleWizard", new CustomLocaleWizardViewModel("work"));
        public IActionResult School() => View();
        public IActionResult SchoolWizard() => View("CustomLocaleWizard", new CustomLocaleWizardViewModel("school"));

    }
}