using FerieCountdown.Classes.Io;
using FerieCountdown.Classes.Locale;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.TimeHandler
{
    public static class TimeMaster
    {
        public static DateTime ValiDate(DateTime t)
        {
            if (t.AddDays(1) < DateTime.UtcNow)
            {
                if (t.Year + 1 < DateTime.UtcNow.Year) return ValiDate(SetYear(t, DateTime.UtcNow.Year));
                else return ValiDate(SetYear(t, t.Year +1));
            }
            else return t;
        }

        public static DateTime MonthlyRecuring(DateTime t)
        {
            while (t.AddDays(1) < DateTime.UtcNow)
            {
                t = t.AddMonths(1);
            }
            return t;
        }

        public static DateTime WeeklyRecuring(DateTime t)
        {
            while (t.AddDays(1) < DateTime.UtcNow)
            {
                t = t.AddDays(7);
            }
            return t;
        }


        public static bool ValiDateBool(DateTime t)
        {
            if (t.AddDays(1) < DateTime.UtcNow) return false;
            else return true;
        }

        static DateTime SetYear(DateTime t, int y)
        {
            return t.AddYears(y - t.Year);
        }


        public static DateTime GenerateDayEndCountdown(Time t)
        { 
            DateTime d = DateTime.UtcNow;
            return new DateTime(d.Year, d.Month, d.Day, t.Hours, t.Minutes, 0);
        }

        public static DateTime GetTodaysEndObj(CountdownLocaleData cld)
        {
            return DateTime.UtcNow.DayOfWeek switch
            {
                DayOfWeek.Monday => GenerateDayEndCountdown(cld.MondayEnd),
                DayOfWeek.Tuesday => GenerateDayEndCountdown(cld.TuesdayEnd),
                DayOfWeek.Wednesday => GenerateDayEndCountdown(cld.WednesdayEnd),
                DayOfWeek.Thursday => GenerateDayEndCountdown(cld.ThursdayEnd),
                DayOfWeek.Friday => GenerateDayEndCountdown(cld.FridayEnd),
                _ => new DateTime(0),
            };
        }

        public static DateTime GetWeekendCountdown(CountdownLocaleData cld)
        {
            DateTime now = DateTime.UtcNow;
            DateTime ret = now.DayOfWeek switch
            {
                DayOfWeek.Monday => now.AddDays(4),
                DayOfWeek.Tuesday => now.AddDays(3),
                DayOfWeek.Wednesday => now.AddDays(2),
                DayOfWeek.Thursday => now.AddDays(1),
                DayOfWeek.Friday => now,
                DayOfWeek.Saturday => now.AddDays(-1),
                DayOfWeek.Sunday => now.AddDays(-2),
                _ => throw new ArgumentOutOfRangeException()
            };
 
            return new DateTime(ret.Year, ret.Month, ret.Day, cld.FridayEnd.Hours, cld.FridayEnd.Minutes, 0);
        }

        public static KeyValuePair<string, DateTime> GetNextHoliday(CountdownLocale Locale)
        {
            try
            {
                string name = "Error";
                DateTime date = DateTime.MaxValue;

                DateTime[] dates = { Locale.LocaleData.AutumnHoliday, Locale.LocaleData.ChristmasHoliday, Locale.LocaleData.WinterHoliday, Locale.LocaleData.EasterHoliday, Locale.LocaleData.SummerHoliday };

                byte i = 0;
                foreach (DateTime d in dates)
                {
                    if (d.CompareTo(DateTime.UtcNow) > 0 && d.CompareTo(date) < 0)
                    {
                        date = d;
                        name = i switch
                        {
                            0 => "Høstferie",
                            1 => "Juleferie",
                            2 => "Vinterferie",
                            3 => "Påskeferie",
                            4 => "Sommerferie",
                            _ => "Error"
                        };
                    }

                }
                return new KeyValuePair<string, DateTime>(name, date);
            }
            catch
            {
                return new KeyValuePair<string, DateTime>("Error", DateTime.MaxValue);
            }
        }

        // Source: https://stackoverflow.com/questions/11154673/get-the-correct-week-number-of-a-given-date
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static int GetDaysToDate(DateTime Date) => (int)Math.Ceiling((Date - DateTime.UtcNow).TotalDays);

        public static int DayEnumToInt(DayOfWeek dayOfWeek) => dayOfWeek switch
        {
            DayOfWeek.Monday => 0,
            DayOfWeek.Tuesday => 1,
            DayOfWeek.Wednesday => 2,
            DayOfWeek.Thursday => 3,
            DayOfWeek.Friday => 4,
            DayOfWeek.Saturday => 5,
            DayOfWeek.Sunday => 6,
            _ => throw new ArgumentException()
        };
    }
}
