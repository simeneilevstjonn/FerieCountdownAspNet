using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerieCountdownWithAuth.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FerieCountdownWithAuth.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Jsongenerator()
        {
            /* 
            //LUNDEHAUGEN
            CountdownLocaleData locale = new CountdownLocaleData
            {
                MondayEnd = new Time(14, 20),
                TuesdayEnd = new Time(13, 15),
                WednesdayEnd = new Time(14, 20),
                ThursdayEnd = new Time(14, 20),
                FridayEnd = new Time(13, 15),
                AutumnHoliday = DateTime.Parse("2019-10-04T11:15:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                ChristmasHoliday = DateTime.Parse("2019-12-21T09:25:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                WinterHoliday = DateTime.Parse("2020-02-21T12:15:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EasterHoliday = DateTime.Parse("2020-04-03T11:15:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                SummerHoliday = DateTime.Parse("2020-06-19T08:25:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind)
            };
            */

            //SKEIANE
            CountdownLocaleData locale = new CountdownLocaleData
            {
                MondayEnd = new Time(13, 55),
                TuesdayEnd = new Time(13, 55),
                WednesdayEnd = new Time(13, 55),
                ThursdayEnd = new Time(13, 55),
                FridayEnd = new Time(14, 25),
                AutumnHoliday = DateTime.Parse("2020-10-02T11:55:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                ChristmasHoliday = DateTime.Parse("2019-12-21T10:00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                WinterHoliday = DateTime.Parse("2020-02-21T12:55:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                EasterHoliday = DateTime.Parse("2020-04-03T12:25:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind),
                SummerHoliday = DateTime.Parse("2020-06-19T11:55:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind)
            };

            ViewData["json"] = JsonConvert.SerializeObject(locale);
            return View();
        }
    }
}