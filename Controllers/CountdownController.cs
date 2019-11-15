using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerieCountdown.Classes;
using FerieCountdown.Classes.Countdowns;
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
        
        private IActionResult TimePassed()
        {
            TempData["WarningAlert"] = "Nedtellingen du forsøkte å besøke er allerede ferdig.";
            return Redirect("/");
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
            ViewData["MetaDescription"] = "Nedtelling til Høstferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id)) 
                {
                    Locale = DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                TempData["WarningAlert"] = "Nedtellingsstedet du forsøkte å bruke eksisterer ikke.";
                ViewData["FullUrl"] = string.Format("{0}://{1}/Countdown/Autumn", Request.Scheme, Request.Host);
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.AutumnHoliday,
                CountdownText = "Nedtelling til Høstferien",
                CountdownEndText = "Høstferie nå!",
                Background = CountdownBackground.Backgrounds["autumncolours"]
            });

        }
        public IActionResult Christmas(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Juleferien";
            ViewData["MetaDescription"] = "Nedtelling til Juleferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id))
                {
                    Locale = DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                TempData["WarningAlert"] = "Nedtellingsstedet du forsøkte å bruke eksisterer ikke.";
                ViewData["FullUrl"] = string.Format("{0}://{1}/Countdown/Christmas", Request.Scheme, Request.Host);
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.ChristmasHoliday,
                CountdownText = "Nedtelling til Juleferien",
                CountdownEndText = "Juleferie nå!",
                Background = CountdownBackground.Backgrounds["christmastreeoutdoorsnow"]
            });
        }
        public IActionResult Winter(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Vinterferien";
            ViewData["MetaDescription"] = "Nedtelling til Vinterferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id))
                {
                    Locale = DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                TempData["WarningAlert"] = "Nedtellingsstedet du forsøkte å bruke eksisterer ikke.";
                ViewData["FullUrl"] = string.Format("{0}://{1}/Countdown/Winter", Request.Scheme, Request.Host);
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.WinterHoliday,
                CountdownText = "Nedtelling til Vinterferien",
                CountdownEndText = "Vinterferie nå!",
                Background = CountdownBackground.Backgrounds["snowwithfx"]    
            });
        }
        public IActionResult Easter(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Påskeferien";
            ViewData["MetaDescription"] = "Nedtelling til Påskeferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id))
                {
                    Locale = DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                TempData["WarningAlert"] = "Nedtellingsstedet du forsøkte å bruke eksisterer ikke.";
                ViewData["FullUrl"] = string.Format("{0}://{1}/Countdown/Easter", Request.Scheme, Request.Host);
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.EasterHoliday,
                CountdownText = "Nedtelling til Påskeferien",
                CountdownEndText = "Påskeferie nå!",
                Background = CountdownBackground.Backgrounds["easter"]
            });
        }
        public IActionResult Summer(string id)
        {
            ViewData["IsHolidayCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Sommerferien";
            ViewData["MetaDescription"] = "Nedtelling til Sommerferien";
            CountdownLocale Locale;
            try
            {
                //Check if a locale override has been provided
                if (!string.IsNullOrEmpty(id))
                {
                    Locale = DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = DbMaster.GetUserLocale(Request);
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.AutumnHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                TempData["WarningAlert"] = "Nedtellingsstedet du forsøkte å bruke eksisterer ikke.";
                ViewData["FullUrl"] = string.Format("{0}://{1}/Countdown/Summer", Request.Scheme, Request.Host);
                return View("SetLocale");
            }
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = Locale.LocaleData.SummerHoliday,
                CountdownText = "Nedtelling til Sommerferien",
                CountdownEndText = "Sommerferie nå!",
                Background = CountdownBackground.Backgrounds["beachboat"]
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
                if (!string.IsNullOrEmpty(id))
                {
                    Locale = DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
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
                TempData["WarningAlert"] = "Nedtellingsstedet du forsøkte å bruke eksisterer ikke.";
                ViewData["FullUrl"] = string.Format("{0}://{1}/Countdown/Dayend", Request.Scheme, Request.Host);
                return View("SetLocale");
            }
            

            if (DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday)
            {
                return View("Countdown", new CountdownViewModel
                {
                    CountdownTime = new DateTime(0),
                    CountdownText = "",
                    CountdownEndText = "Nå er det helg!",
                    Background = CountdownBackground.Backgrounds["classroom"],
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
                    Background = CountdownBackground.Backgrounds["classroom"],
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
                if (!string.IsNullOrEmpty(id))
                {
                    Locale = DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
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
                TempData["WarningAlert"] = "Nedtellingsstedet du forsøkte å bruke eksisterer ikke.";
                ViewData["FullUrl"] = string.Format("{0}://{1}/Countdown/Weekend", Request.Scheme, Request.Host);
                return View("SetLocale");
            }
            

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til helg",
                CountdownEndText = "Helg nå!",
                Background = CountdownBackground.Backgrounds["classroom"],
                UseLocalTime = true
            });
        }

        //IActionresult() for custom countdown
        public IActionResult Custom(string id)
        {
            //TODO add checking if user is allowed to view countdown

            if (string.IsNullOrEmpty(id)) return Redirect("/");
            CustomCountdown countdowndata = DbMaster.GetCustomCountdown(id);
            ViewData["Title"] = countdowndata.CountdownText;
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = countdowndata.CountdownTime,
                CountdownText = countdowndata.CountdownText,
                CountdownEndText = countdowndata.CountdownEndText,
                UseLocalTime = countdowndata.UseLocalTime,
                Background = countdowndata.Background
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
                UseLocalTime = true,
                Background = CountdownBackground.Backgrounds["fireworks"]
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
                UseLocalTime = true,
                Background = CountdownBackground.Backgrounds["jackolantern"]
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
                UseLocalTime = true,
                Background = CountdownBackground.Backgrounds["17may"]
            }); ;
        }
        public IActionResult Christmaseve()
        {
            ViewData["IsOtherCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Julaften";
            DateTime cdtime = TimeMaster.ValiDate(DateTime.Parse("2019-12-24T00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind));
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til Julaften",
                CountdownEndText = "Julaften i dag!",
                UseLocalTime = true,
                Background = CountdownBackground.Backgrounds["christmasinterior"]
            }); ;
        }

        public IActionResult SetLocale()
        {
            ViewData["FullUrl"] = string.Format("{0}://{1}/", Request.Scheme, Request.Host);
            return View();
        }
    }
}