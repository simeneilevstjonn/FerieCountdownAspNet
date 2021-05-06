using FerieCountdown.Classes.LessonCounter;
using FerieCountdown.Classes.TimeHandler;
using FerieCountdown.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FerieCountdown.Classes.LessonCounter.Subjects;

namespace FerieCountdown.Controllers
{
    public class LessonsController : Controller
    {
        public IActionResult Index()
        {
            ViewData["IsLessonCounter"] = "true";
            return Redirect("/Lessons/Counter/10F");
        }

        public IActionResult Counter(string id)
        {
            ViewData["IsLessonCounter"] = "true";

            // Not found if id is not 10F
            if (id != "10F") return NotFound();
            ViewData["MetaDescription"] = "Oversikt over gjenværende timer i skoleåret 2020/21 for 10F på Lundehaugen Ungdomsskole.";

            ViewData["Title"] = "Resterende timer - 10F Lundehaugen Ungdomsskole";

            // Create the LessonCounterClass
            LessonCounterClass LCC10F = new LessonCounterClass
            {
                TimeSchedule = new List<List<Time>>
                {
                    new List<Time>
                    {
                        new Time(6,15),
                        new Time(7,25),
                        new Time(9,5),
                        new Time(10,15),
                        new Time(11,20)
                    },
                    new List<Time>
                    {
                        new Time(6,15),
                        new Time(7,25),
                        new Time(9,5),
                        new Time(10,15),
                    },
                    new List<Time>
                    {
                        new Time(6,15),
                        new Time(7,25),
                        new Time(9,5),
                        new Time(10,15),
                        new Time(11,20)
                    },
                    new List<Time>
                    {
                        new Time(6,15),
                        new Time(7,25),
                        new Time(9,5),
                        new Time(10,15),
                        new Time(11,20)
                    },
                    new List<Time>
                    {
                        new Time(6,15),
                        new Time(7,25),
                        new Time(9,5),
                        new Time(10,15),
                    }
                },
                SubjectSchedule = new List<List<Subject>>
                {
                    new List<Subject>
                    {
                        Subject.SocialStudies,
                        Subject.Mathematics,
                        Subject.Science,
                        Subject.Norwegian,
                        Subject.Norwegian
                    },
                    new List<Subject>
                    {
                        Subject.SocialStudies,
                        Subject.PhysicalEducation,
                        Subject.English,
                        Subject.Music
                    },
                    new List<Subject>
                    {
                        Subject.PhysicalEducation,
                        Subject.Language,
                        Subject.ArtsAndCrafts,
                        Subject.ArtsAndCrafts,
                        Subject.Norwegian
                    },
                    new List<Subject>
                    {
                        Subject.Language,
                        Subject.Mathematics,
                        Subject.English,
                        Subject.Religion,
                        Subject.Choice
                    },
                    new List<Subject>
                    {
                        Subject.Mathematics,
                        Subject.Norwegian,
                        Subject.SocialStudies,
                        Subject.Science
                    }
                },
                /*LastDate = new DateTime(2021, 6, 17),*/
                LastDate = new DateTime(2021, 6, 2),
                OverrideSchedule = new Dictionary<DateTime, List<Subject>>
                {
                    {
                        new DateTime(2021, 05, 12),
                        new List<Subject>
                        {
                            Subject.Mathematics,
                            Subject.Mathematics,
                            Subject.Mathematics,
                            Subject.Mathematics
                        }
                    },
                    {
                        new DateTime(2021, 05, 13),
                        null
                    },
                    {
                        new DateTime(2021, 05, 14),
                        null
                    },
                    {
                        new DateTime(2021, 05, 17),
                        null
                    },
                    {
                        new DateTime(2021, 05, 24),
                        null
                    },
                    // Prøvemuntlig
                    {
                        new DateTime(2021, 05, 18),
                        null
                    },
                    {
                        new DateTime(2021, 05, 19),
                        null
                    },
                    {
                        new DateTime(2021, 05, 20),
                        null
                    },
                    // Muntlig
                    {
                        new DateTime(2021, 05, 30),
                        null
                    },
                    {
                        new DateTime(2021, 06, 01),
                        null
                    },
                    {
                        new DateTime(2021, 06, 02),
                        null
                    },
                }
            };

            // Return view with model
            return View("LessonCounter", new LessonCounterViewModel
            {
                RemainingCount = LCC10F.RemainingLessons
            });
        }
    }
}
