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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FerieCountdown.Controllers
{
    public class SetLocaleController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Work() => View("CustomLocaleWizard", new CustomLocaleWizardViewModel("work"));
        public IActionResult School() => View();
        public IActionResult SchoolWizard() => View("CustomLocaleWizard", new CustomLocaleWizardViewModel("school"));

        [HttpPost]
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

            DbMaster.SqlQuery(string.Format("DELETE FROM [dbo].[CustomLocales] WHERE UserId = N'{0}'; INSERT INTO [dbo].[CustomLocales] ([UserId], [Data], [IsWork]) VALUES (N'{0}', N'{1}', {2});", User.FindFirstValue(ClaimTypes.NameIdentifier), JsonConvert.SerializeObject(cld), IsWork));

            if (Request.Query["redirecturi"].ToString() == null) return Redirect("/");
            else return Redirect(Request.Form["RedirectUri"]);
        }

    }
}