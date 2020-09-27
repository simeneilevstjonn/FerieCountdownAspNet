using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FerieCountdown.Classes.Io;
using FerieCountdown.Classes.Locale;
using FerieCountdown.Classes.TimeHandler;
using FerieCountdown.Models;
using FerieCountdownWithAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FerieCountdown.Controllers
{
    public class SetLocaleController : Controller
    {
        public IActionResult Index() => View(new SetLocaleViewModel(Request.Query["redirecturl"]));
        [Authorize]
        public IActionResult Work() => View("CustomLocaleWizard", new CustomLocaleWizardViewModel("work", Request.Query["redirecturl"]));
        public IActionResult School() => View(new SetLocaleViewModel(Request.Query["redirecturl"]));
        [Authorize]
        public IActionResult SchoolWizard() => View("CustomLocaleWizard", new CustomLocaleWizardViewModel("school", Request.Query["redirecturl"]));

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUserLocale()
        {
            int IsWork = (string)Request.Form["IsWork"] switch
            {
                "1" => 1,
                _ => 0
            };
            CountdownLocaleData cld = new CountdownLocaleData
            {
                MondayEnd = new Time(Request.Form["mon"]),
                TuesdayEnd = new Time(Request.Form["tue"]),
                WednesdayEnd = new Time(Request.Form["wed"]),
                ThursdayEnd = new Time(Request.Form["thu"]),
                FridayEnd = new Time(Request.Form["fri"]),
                AutumnHoliday = DateTime.Parse(Request.Form["autumn"], null, DateTimeStyles.RoundtripKind),
                ChristmasHoliday = DateTime.Parse(Request.Form["christmas"], null, DateTimeStyles.RoundtripKind),
                WinterHoliday = DateTime.Parse(Request.Form["winter"], null, DateTimeStyles.RoundtripKind),
                EasterHoliday = DateTime.Parse(Request.Form["easter"], null, DateTimeStyles.RoundtripKind),
                SummerHoliday = DateTime.Parse(Request.Form["summer"], null, DateTimeStyles.RoundtripKind)
            };

            Startup._DbMaster.SqlQuery(string.Format("DELETE FROM [dbo].[CustomLocales] WHERE UserId = N'{0}'; INSERT INTO [dbo].[CustomLocales] ([UserId], [Data], [IsWork]) VALUES (N'{0}', N'{1}', {2});", User.FindFirstValue(ClaimTypes.NameIdentifier), JsonConvert.SerializeObject(cld), IsWork));

            return Redirect(Request.Form["redirectUrl"]);
        }

    }
}