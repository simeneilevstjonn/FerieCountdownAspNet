using FerieCountdown.Classes.TimeHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FerieCountdown.Classes.LessonCounter.Subjects;

namespace FerieCountdown.Classes.LessonCounter
{
    public class LessonCounterClass
    {
        public List<List<Time>> TimeSchedule { get; set; }
        public List<List<Subject>> SubjectSchedule { get; set; }
        public DateTime LastDate { get; set; }
        public Dictionary<DateTime, List<Subject>> OverrideSchedule { get; set; }
        public Dictionary<Subject, int> RemainingLessons
        {
            get
            {
                // Create the dictionary
                Dictionary<Subject, int> Remaining = new Dictionary<Subject, int>();

                // Iterate through each subject
                foreach (Subject subject in (Subject[])Enum.GetValues(typeof(Subject)))
                {
                    // Add to dictionary
                    Remaining.Add(subject, 0);
                }

                // Get the current day
                DateTime Today = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);

                // Check if today is past LastDate
                if (DateTime.Compare(Today, LastDate) > 0) return Remaining;

                // Add lessons not yet started today
                if (Today.DayOfWeek != DayOfWeek.Sunday && Today.DayOfWeek != DayOfWeek.Saturday)
                {
                    // Get today's schedule
                    List<Subject> Schedule = SubjectSchedule[TimeMaster.DayEnumToInt(Today.DayOfWeek)];

                    // Get today's times
                    List<Time> Times = TimeSchedule[TimeMaster.DayEnumToInt(Today.DayOfWeek)];

                    // Check if there is an override for today
                    if (OverrideSchedule.ContainsKey(Today))
                    {
                        Schedule = OverrideSchedule[Today];
                    }

                    // Iterate through each lesson on schedule
                    foreach ((Subject subject, Time time) in Schedule.Zip(Times, (x, y) => new Tuple<Subject, Time>(x, y)).ToList())
                    {
                        // Check that the lesson has not already started
                        if (time > new Time(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute))
                        {
                            // Increment subject counter
                            Remaining[subject]++;
                        }
                    }  
                }

                // Iterate through the rest of the days before the last date
                for (DateTime Day = Today.AddDays(1); DateTime.Compare(Day, LastDate) <= 0; Day = Day.AddDays(1))
                {
                    // Check if day is a week day
                    if (Day.DayOfWeek != DayOfWeek.Sunday && Day.DayOfWeek != DayOfWeek.Saturday)
                    {
                        // Get the day's schedule
                        List<Subject> Schedule = SubjectSchedule[TimeMaster.DayEnumToInt(Day.DayOfWeek)];

                        // Check if there is an override for the day
                        if (OverrideSchedule.ContainsKey(Day))
                        {
                            Schedule = OverrideSchedule[Day];
                        }

                        // Check if schedule is not null
                        if (Schedule != null)
                        {
                            // Iterate through each lesson on schedule
                            foreach (Subject subject in Schedule)
                            {
                                // Increment subject counter
                                Remaining[subject]++;
                            }
                        }
                    }
                }

                // Return
                return Remaining;
            }
        }
    }
}
