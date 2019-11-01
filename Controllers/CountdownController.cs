using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerieCountdown.Classes;
using FerieCountdown.Classes.Io;
using FerieCountdown.Classes.Locale;
using FerieCountdown.Classes.TimeHandler;
using FerieCountdown.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FerieCountdown.Controllers
{
    public class CountdownController : Controller
    {
        private IActionResult Error(string message)
        {
            return View("Error", new CountdownErrorViewModel
            {
                Message = message
            });
        } 

        private void InitSharedVars()
        {
            ViewData["FullUrl"] = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host, Request.Path);
        }

        //Define holiday countdowns
        public IActionResult Autumn(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Høstferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) Locale = DbMaster.GetLocale(id);
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return Redirect("/");
            }
            catch (NullReferenceException)
            {
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel 
            {
                CountdownTime = Locale.LocaleData.AutumnHoliday,
                CountdownText = "Nedtelling til Høstferien",
                CountdownEndText = "Høstferie nå!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/a19/static.jpg",
                UseCCCText = true
            });

        }
        public IActionResult Christmas(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Juleferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) Locale = DbMaster.GetLocale(id);
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return Redirect("/");
            }
            catch (NullReferenceException)
            {
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.ChristmasHoliday,
                CountdownText = "Nedtelling til Juleferien",
                CountdownEndText = "Juleferie nå!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/c19/static.jpg",
                UseCCCText = false,
                HtmlAppend = "<div class=\"snow-container\"><div class=\"snow foreground\"></div><div class=\"snow foreground layered\"></div><div class=\"snow middleground\"></div><div class=\"snow middleground layered\"></div><div class=\"snow background\"></div><div class=\"snow background layered\"></div></div>",
                CssAppend = ".snow-container{position:absolute;left:0;height:80%;width:100%;max-width:100%;top:0;overflow:hidden;z-index:2;pointer-events:none}.snow{display:block;position:absolute;z-index:2;top:0;right:0;bottom:0;left:0;pointer-events:none;-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0);-webkit-animation:snow linear infinite;animation:snow linear infinite}.snow.foreground{background-image:url(https://static.feriecountdown.com/resources/snow/snow-large.png);-webkit-animation-duration:15s;animation-duration:15s}.snow.foreground.layered{-webkit-animation-delay:7.5s;animation-delay:7.5s}.snow.middleground{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-medium.png);-webkit-animation-duration:20s;animation-duration:20s}.snow.middleground.layered{-webkit-animation-delay:10s;animation-delay:10s}.snow.background{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-small.png);-webkit-animation-duration:30s;animation-duration:30s}.snow.background.layered{-webkit-animation-delay:15s;animation-delay:15s}@-webkit-keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}@keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}"
            });
        }
        public IActionResult Winter(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Vinterferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) Locale = DbMaster.GetLocale(id);
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return Redirect("/");
            }
            catch (NullReferenceException)
            {
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.WinterHoliday,
                CountdownText = "Nedtelling til Vinterferien",
                CountdownEndText = "Vinterferie nå!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/w20/animbg.jpg",
                UseCCCText = false,
                HtmlAppend = "<div class=\"snow-container\"><div class=\"snow foreground\"></div><div class=\"snow foreground layered\"></div><div class=\"snow middleground\"></div><div class=\"snow middleground layered\"></div><div class=\"snow background\"></div><div class=\"snow background layered\"></div></div>",
                CssAppend = ".snow-container{position:absolute;left:0;height:80%;width:100%;max-width:100%;top:0;overflow:hidden;z-index:2;pointer-events:none}.snow{display:block;position:absolute;z-index:2;top:0;right:0;bottom:0;left:0;pointer-events:none;-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0);-webkit-animation:snow linear infinite;animation:snow linear infinite}.snow.foreground{background-image:url(https://static.feriecountdown.com/resources/snow/snow-large.png);-webkit-animation-duration:15s;animation-duration:15s}.snow.foreground.layered{-webkit-animation-delay:7.5s;animation-delay:7.5s}.snow.middleground{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-medium.png);-webkit-animation-duration:20s;animation-duration:20s}.snow.middleground.layered{-webkit-animation-delay:10s;animation-delay:10s}.snow.background{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-small.png);-webkit-animation-duration:30s;animation-duration:30s}.snow.background.layered{-webkit-animation-delay:15s;animation-delay:15s}@-webkit-keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}@keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}"
            });
        }
        public IActionResult Easter(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Påskeferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) Locale = DbMaster.GetLocale(id);
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return Redirect("/");
            }
            catch (NullReferenceException)
            {
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.EasterHoliday,
                CountdownText = "Nedtelling til Påskeferien",
                CountdownEndText = "Påskeferie nå!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/e20/static.jpg",
                UseCCCText = false
            });
        }
        public IActionResult Summer(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Sommerferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) Locale = DbMaster.GetLocale(id);
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return Redirect("/");
            }
            catch (NullReferenceException)
            {
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.SummerHoliday,
                CountdownText = "Nedtelling til Sommerferien",
                CountdownEndText = "Sommerferie nå!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/s20/static.jpg",
                UseCCCText = false
            });
        }

        //Define day end and weekend countdowns.
        public IActionResult Dayend(string id)
        {
            ViewData["IsSchooldayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til skoledagens slutt";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) Locale = DbMaster.GetLocale(id);
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            InitSharedVars();
            DateTime cdtime;
            try
            {
                cdtime = TimeMaster.GetTodaysEndObj(Locale.LocaleData);
            }
            catch (NullReferenceException)
            {
                return View("SetLocale");
            }
            

            if (DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday)
            {
                return View("Countdown", new CountdownViewModel
                {
                    CountdownTime = new DateTime(0),
                    CountdownText = "",
                    CountdownEndText = "Nå er det helg!",
                    BackgroundPath = "https://static.feriecountdown.com/resources/background/de/static.jpg",
                    UseCCCText = false,
                    UseLocalTime = true
                });
            }
            else
            {
                return View("Countdown", new CountdownViewModel
                {
                    CountdownTime = cdtime,
                    CountdownText = "Skoledagen slutter om:",
                    CountdownEndText = "Skoledagen er slutt!",
                    BackgroundPath = "https://static.feriecountdown.com/resources/background/de/static.jpg",
                    UseCCCText = false,
                    UseLocalTime = true
                });
            }
        }

        public IActionResult Weekend(string id)
        {
            ViewData["IsSchooldayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til helg";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) Locale = DbMaster.GetLocale(id);
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            InitSharedVars();
            DateTime cdtime;
            try
            {
                cdtime = TimeMaster.GetWeekendCountdown(Locale.LocaleData);
            }
            catch (NullReferenceException)
            {
                return View("SetLocale");
            }
            

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til helg",
                CountdownEndText = "Helg nå!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/de/static.jpg",
                UseCCCText = false,
                UseLocalTime = true
            });
        }

        //Define personal celebrations
        public IActionResult Birthday()
        {
            ViewData["IsPersonalCelebration"] = "true";
            if (string.IsNullOrEmpty(Request.Query["d"]) || string.IsNullOrEmpty(Request.Query["n"])) return Error("Required query string parameters n and/or d are missing.");
            ViewData["IsPersonalCountdown"] = "true";
            string pn = Request.Query["n"];
            string cdname = char.ToUpper(pn[0]) + pn.Substring(1);
            if (pn.ToCharArray()[pn.Length - 1] != 's') cdname += "s";
            ViewData["Title"] = string.Format("Nedtelling til {0} bursdag", cdname);
            DateTime cdtime = TimeMaster.ValiDate(DateTime.Parse(Request.Query["d"] + "z", null, System.Globalization.DateTimeStyles.RoundtripKind));
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = string.Format("Nedtelling til {0} bursdag", cdname),
                CountdownEndText = string.Format("Gratulerer med dagen {0}!", char.ToUpper(pn[0]) + pn.Substring(1)),
                BackgroundPath = "https://static.feriecountdown.com/resources/background/bd/static.jpg",
                UseCCCText = true,
                UseLocalTime = true
            }); ;
        }
        public IActionResult Confirmation()
        {
            ViewData["IsPersonalCelebration"] = "true";
            if (string.IsNullOrEmpty(Request.Query["d"]) || string.IsNullOrEmpty(Request.Query["n"])) return Error("Required query string parameters n and/or d are missing.");
            ViewData["IsPersonalCountdown"] = "true";
            string pn = Request.Query["n"];
            string cdname = char.ToUpper(pn[0]) + pn.Substring(1);
            if (pn.ToCharArray()[pn.Length - 1] != 's') cdname += "s";
            ViewData["Title"] = string.Format("Nedtelling til {0} konfirmasjon", cdname);
            DateTime cdtime = DateTime.Parse(Request.Query["d"] + "z", null, System.Globalization.DateTimeStyles.RoundtripKind);
            if (!TimeMaster.ValiDateBool(cdtime)) return Redirect("/");
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = string.Format("Nedtelling til {0} konfirmasjon", cdname),
                CountdownEndText = string.Format("I dag er {0} konfirmasjon!", cdname),
                //BackgroundPath = "https://static.feriecountdown.com/resources/background/bd/static.jpg",
                //UseCCCText = true,
                UseLocalTime = true
            }); ;
        }
        public IActionResult Wedding()
        {
            ViewData["IsPersonalCelebration"] = "true";
            if (string.IsNullOrEmpty(Request.Query["d"]) || string.IsNullOrEmpty(Request.Query["n0"]) || string.IsNullOrEmpty(Request.Query["n1"])) return Error("Required query string parameters n0, n1 and/or d are missing.");
            ViewData["IsPersonalCountdown"] = "true";
            string p0n = Request.Query["n0"];
            string p1n = Request.Query["n1"];
            string cdname1 = char.ToUpper(p1n[0]) + p1n.Substring(1);
            if (p1n.ToCharArray()[p1n.Length - 1] != 's') cdname1 += "s";
            ViewData["Title"] = string.Format("Nedtelling til {0} og {1} bryllup", p0n, cdname1);
            DateTime cdtime = DateTime.Parse(Request.Query["d"] + "z", null, System.Globalization.DateTimeStyles.RoundtripKind);
            if (!TimeMaster.ValiDateBool(cdtime)) return Redirect("/");
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = string.Format("Nedtelling til {0} og {1} bryllup", p0n, cdname1),
                CountdownEndText = string.Format("I dag er {0} og {1} bryllup", p0n, cdname1),
                //BackgroundPath = "https://static.feriecountdown.com/resources/background/bd/static.jpg",
                //UseCCCText = true,
                UseLocalTime = true
            }); ;
        }



        //Define other countdowns
        public IActionResult Newyear() 
        {
            ViewData["IsOtherCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Nyttår";
            DateTime cdtime = TimeMaster.ValiDate(DateTime.Parse("2020-01-01T00:00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind));
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til Nyttår",
                CountdownEndText = string.Format("{0} nå!", cdtime.Year),
                BackgroundPath = "https://static.feriecountdown.com/resources/background/ny/static.png",
                UseCCCText = true,
                UseLocalTime = true
            }); ;
        }
        public IActionResult Halloween()
        {
            ViewData["IsOtherCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Halloween";
            DateTime cdtime = TimeMaster.ValiDate(DateTime.Parse("2019-10-31T00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind));
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til Halloween",
                CountdownEndText = "Halloween i dag!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/hw/halloween.jpg",
                UseCCCText = true,
                UseLocalTime = true
            }); ;
        }
        public IActionResult Seventeenmay()
        {
            ViewData["IsOtherCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til 17. mai";
            DateTime cdtime = TimeMaster.ValiDate(DateTime.Parse("2020-05-17T00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind));
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til 17. mai",
                CountdownEndText = "17. mai i dag!",
                BackgroundPath = "https://static.feriecountdown.com/resources/background/17may.jpg",
                UseCCCText = true,
                UseLocalTime = true
            }); ;
        }

        public IActionResult SetLocale()
        {
            return View();
        }

        public IActionResult TestUserData()
        {
            return Error(JsonConvert.SerializeObject(DbMaster.GetUserLocale(Request)));
        }

        public IActionResult TestAllLocales()
        {
            return Error(JsonConvert.SerializeObject(DbMaster.GetAllLocales()));
        }

    }
}