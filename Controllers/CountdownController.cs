using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FerieCountdown.Classes;
using FerieCountdown.Classes.Countdowns;
using FerieCountdown.Classes.Io;
using FerieCountdown.Classes.Locale;
using FerieCountdown.Classes.TimeHandler;
using FerieCountdown.Models;
using FerieCountdownWithAuth;
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
            string path = Request.Path;
            string cac = path.Substring(1);
            string action = cac.Substring(cac.IndexOf('/') + 1);
            ViewData["BaseUrl"] = string.Format("{0}://{1}/Countdown/{2}", Request.Scheme, Request.Host, action);
            ViewData["Locale"] = Request.Cookies["locale"];
        }
        private void InitSharedVars(string locale)
        {
            InitSharedVars();
            if (!string.IsNullOrEmpty(locale))
            {
                ViewData["Locale"] = locale;
                string path = Request.Path;
                string cac = path.Substring(1);
                string action = cac.Substring(cac.IndexOf('/') + 1);
                action = action.Substring(0, action.IndexOf('/'));
                ViewData["BaseUrl"] = string.Format("{0}://{1}/Countdown/{2}", Request.Scheme, Request.Host, action);
            }
            
            ViewData["UseLocaleOption"] = "true";
        }

        private IActionResult SetLocale(string ReturnUrl) => Redirect($"/SetLocale?redirecturl={ReturnUrl}");

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
                    Locale = Startup._DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = Startup._DbMaster.GetUserLocale(Request, User.FindFirstValue(ClaimTypes.NameIdentifier));
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
                return SetLocale("/Countdown/Autumn");
            }
            InitSharedVars(id);

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
                    Locale = Startup._DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = Startup._DbMaster.GetUserLocale(Request, User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.ChristmasHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                return SetLocale("/Countdown/Christmas");
            }
            InitSharedVars(id);

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
                    Locale = Startup._DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = Startup._DbMaster.GetUserLocale(Request, User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.WinterHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                return SetLocale("/Countdown/Winter");
            }
            InitSharedVars(id);

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
                    Locale = Startup._DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = Startup._DbMaster.GetUserLocale(Request, User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.EasterHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                return SetLocale("/Countdown/Easter");
            }
            InitSharedVars(id);

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
                    Locale = Startup._DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = Startup._DbMaster.GetUserLocale(Request, User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            try
            {
                if (!TimeMaster.ValiDateBool(Locale.LocaleData.SummerHoliday)) return TimePassed();
            }
            catch (NullReferenceException)
            {
                return SetLocale("/Countdown/Summer");
            }
            InitSharedVars(id);

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
            
            CountdownLocale Locale;

            //Check if a locale override has been provided
            if (!string.IsNullOrEmpty(id))
            {
                Locale = Startup._DbMaster.GetLocale(id);
                ViewData["MetaDescription"] += " på " + Locale.School;
            }
            else Locale = Startup._DbMaster.GetUserLocale(Request, User.FindFirstValue(ClaimTypes.NameIdentifier));

            InitSharedVars(id);
            DateTime cdtime;
            try
            {
                cdtime = TimeMaster.GetTodaysEndObj(Locale.LocaleData);
            }
            catch (NullReferenceException)
            {
                return SetLocale("/Countdown/Dayend");
            }

            ViewData["Title"] = Locale.IsWork switch
            {
                false => "Nedtelling til skoledagens slutt",
                true => "Nedtelling til arbeidsdagens slutt"
            };

            if (DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday)
            {
                return View("Countdown", new CountdownViewModel
                {
                    CountdownTime = new DateTime(0),
                    CountdownText = "",
                    CountdownEndText = "Nå er det helg!",
                    Background = Locale.IsWork switch 
                    { 
                        true => CountdownBackground.Backgrounds["office"],
                        false => CountdownBackground.Backgrounds["classroom"]
                    },
                    UseLocalTime = true
                });
            }
            else
            {
                return View("Countdown", new CountdownViewModel
                {
                    CountdownTime = cdtime,
                    CountdownText = Locale.IsWork switch
                    {
                        false => "Skoledagen slutter om:",
                        true => "Arbeidsdagen slutter om:"
                    },
                    CountdownEndText = Locale.IsWork switch
                    {
                        false => "Skoledagen er slutt!",
                        true => "Arbeidsdagen er slutt!"
                    },
                    Background = Locale.IsWork switch
                    {
                        true => CountdownBackground.Backgrounds["office"],
                        false => CountdownBackground.Backgrounds["classroom"]
                    },
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
                    Locale = Startup._DbMaster.GetLocale(id);
                    ViewData["MetaDescription"] += " på " + Locale.School;
                }
                else Locale = Startup._DbMaster.GetUserLocale(Request, User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
            InitSharedVars(id);
            DateTime cdtime;
            try
            {
                cdtime = TimeMaster.GetWeekendCountdown(Locale.LocaleData);
            }
            catch (NullReferenceException)
            {
                return SetLocale("/Countdown/Weekend");
            }
            

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til helg",
                CountdownEndText = "Helg nå!",
                Background = Locale.IsWork switch
                {
                    true => CountdownBackground.Backgrounds["office"],
                    false => CountdownBackground.Backgrounds["classroom"]
                },
                UseLocalTime = true
            });
        }

        //IActionresult() for custom countdown
        public IActionResult Custom(string id)
        {

            if (string.IsNullOrEmpty(id)) return Redirect("/");
            CustomCountdown countdowndata = Startup._DbMaster.GetCustomCountdown(id);
            ViewData["Title"] = countdowndata.CountdownText;
            ViewData["IsCustomCountdown"] = "true";
            InitSharedVars();

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = countdowndata.CountdownTime,
                CountdownText = countdowndata.CountdownText,
                CountdownEndText = countdowndata.CountdownEndText,
                UseLocalTime = countdowndata.UseLocalTime,
                Background = countdowndata.Background
            });
        }


        //Define other countdowns
        public IActionResult Newyear() 
        {
            ViewData["IsOtherCountdown"] = "true";
            ViewData["Title"] = "Nedtelling til Nyttår";
            DateTime cdtime = TimeMaster.ValiDate(DateTime.Parse("2020-01-01T00:00:00Z", null, System.Globalization.DateTimeStyles.RoundtripKind));
            InitSharedVars();

            CountdownBackground bg = CountdownBackground.Backgrounds["fireworks"];
            bg.Html = "<div class=\"pyro\" style=\"display: none; height: 100vh; top: 0; position: fixed; left: 0; width: 100vw; z-index: -1\" id=\"fireworks\"><div class=\"before\"></div><div class=\"after\"></div></div>";
            bg.Css = ".pyro > .before, .pyro > .after{position: absolute; width: 5px; height: 5px; border-radius: 50%; box-shadow: -120px -218.66667px blue, 248px -16.66667px #00ff84, 190px 16.33333px #002bff, -113px -308.66667px #ff009d, -109px -287.66667px #ffb300, -50px -313.66667px #ff006e, 226px -31.66667px #ff4000, 180px -351.66667px #ff00d0, -12px -338.66667px #00f6ff, 220px -388.66667px #99ff00, -69px -27.66667px #ff0400, -111px -339.66667px #6200ff, 155px -237.66667px #00ddff, -152px -380.66667px #00ffd0, -50px -37.66667px #00ffdd, -95px -175.66667px #a6ff00, -88px 10.33333px #0d00ff, 112px -309.66667px #005eff, 69px -415.66667px #ff00a6, 168px -100.66667px #ff004c, -244px 24.33333px #ff6600, 97px -325.66667px #ff0066, -211px -182.66667px #00ffa2, 236px -126.66667px #b700ff, 140px -196.66667px #9000ff, 125px -175.66667px #00bbff, 118px -381.66667px #ff002f, 144px -111.66667px #ffae00, 36px -78.66667px #f600ff, -63px -196.66667px #c800ff, -218px -227.66667px #d4ff00, -134px -377.66667px #ea00ff, -36px -412.66667px #ff00d4, 209px -106.66667px #00fff2, 91px -278.66667px #000dff, -22px -191.66667px #9dff00, 139px -392.66667px #a6ff00, 56px -2.66667px #0099ff, -156px -276.66667px #ea00ff, -163px -233.66667px #00fffb, -238px -346.66667px #00ff73, 62px -363.66667px #0088ff, 244px -170.66667px #0062ff, 224px -142.66667px #b300ff, 141px -208.66667px #9000ff, 211px -285.66667px #ff6600, 181px -128.66667px #1e00ff, 90px -123.66667px #c800ff, 189px 70.33333px #00ffc8, -18px -383.66667px #00ff33, 100px -6.66667px #ff008c; -moz-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards; -webkit-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards; -o-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards; -ms-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards; animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards;}.pyro > .after{-moz-animation-delay: 1.25s, 1.25s, 1.25s; -webkit-animation-delay: 1.25s, 1.25s, 1.25s; -o-animation-delay: 1.25s, 1.25s, 1.25s; -ms-animation-delay: 1.25s, 1.25s, 1.25s; animation-delay: 1.25s, 1.25s, 1.25s; -moz-animation-duration: 1.25s, 1.25s, 6.25s; -webkit-animation-duration: 1.25s, 1.25s, 6.25s; -o-animation-duration: 1.25s, 1.25s, 6.25s; -ms-animation-duration: 1.25s, 1.25s, 6.25s; animation-duration: 1.25s, 1.25s, 6.25s;}@-webkit-keyframes bang{from{box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;}}@-moz-keyframes bang{from{box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;}}@-o-keyframes bang{from{box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;}}@-ms-keyframes bang{from{box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;}}@keyframes bang{from{box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;}}@-webkit-keyframes gravity{to{transform: translateY(200px); -moz-transform: translateY(200px); -webkit-transform: translateY(200px); -o-transform: translateY(200px); -ms-transform: translateY(200px); opacity: 0;}}@-moz-keyframes gravity{to{transform: translateY(200px); -moz-transform: translateY(200px); -webkit-transform: translateY(200px); -o-transform: translateY(200px); -ms-transform: translateY(200px); opacity: 0;}}@-o-keyframes gravity{to{transform: translateY(200px); -moz-transform: translateY(200px); -webkit-transform: translateY(200px); -o-transform: translateY(200px); -ms-transform: translateY(200px); opacity: 0;}}@-ms-keyframes gravity{to{transform: translateY(200px); -moz-transform: translateY(200px); -webkit-transform: translateY(200px); -o-transform: translateY(200px); -ms-transform: translateY(200px); opacity: 0;}}@keyframes gravity{to{transform: translateY(200px); -moz-transform: translateY(200px); -webkit-transform: translateY(200px); -o-transform: translateY(200px); -ms-transform: translateY(200px); opacity: 0;}}@-webkit-keyframes position{0%, 19.9%{margin-top: 10%; margin-left: 40%;}20%, 39.9%{margin-top: 40%; margin-left: 30%;}40%, 59.9%{margin-top: 20%; margin-left: 70%;}60%, 79.9%{margin-top: 30%; margin-left: 20%;}80%, 99.9%{margin-top: 30%; margin-left: 80%;}}@-moz-keyframes position{0%, 19.9%{margin-top: 10%; margin-left: 40%;}20%, 39.9%{margin-top: 40%; margin-left: 30%;}40%, 59.9%{margin-top: 20%; margin-left: 70%;}60%, 79.9%{margin-top: 30%; margin-left: 20%;}80%, 99.9%{margin-top: 30%; margin-left: 80%;}}@-o-keyframes position{0%, 19.9%{margin-top: 10%; margin-left: 40%;}20%, 39.9%{margin-top: 40%; margin-left: 30%;}40%, 59.9%{margin-top: 20%; margin-left: 70%;}60%, 79.9%{margin-top: 30%; margin-left: 20%;}80%, 99.9%{margin-top: 30%; margin-left: 80%;}}@-ms-keyframes position{0%, 19.9%{margin-top: 10%; margin-left: 40%;}20%, 39.9%{margin-top: 40%; margin-left: 30%;}40%, 59.9%{margin-top: 20%; margin-left: 70%;}60%, 79.9%{margin-top: 30%; margin-left: 20%;}80%, 99.9%{margin-top: 30%; margin-left: 80%;}}@keyframes position{0%, 19.9%{margin-top: 10%; margin-left: 40%;}20%, 39.9%{margin-top: 40%; margin-left: 30%;}40%, 59.9%{margin-top: 20%; margin-left: 70%;}60%, 79.9%{margin-top: 30%; margin-left: 20%;}80%, 99.9%{margin-top: 30%; margin-left: 80%;}}";

            return View("Countdown", new CountdownViewModel
            {
                CountdownTime = cdtime,
                CountdownText = "Nedtelling til Nyttår",
                CountdownEndText = string.Format("{0} nå!", cdtime.Year),
                UseLocalTime = true,
                Background = bg,
                ExtraStopJs = "document.getElementById(\"fireworks\").style.display = \"block\";"
            });
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
    }
}