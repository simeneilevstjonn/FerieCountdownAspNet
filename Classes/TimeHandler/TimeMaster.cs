using FerieCountdown.Classes.Io;
using FerieCountdown.Classes.Locale;
using System;
using System.Collections.Generic;
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
                if (t.Year + 1 < DateTime.UtcNow.Year)
                {
                    return ValiDate(SetYear(t, DateTime.UtcNow.Year));
                }
                else
                {
                    return ValiDate(SetYear(t, t.Year + 1));
                }
            }
            else return t;
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
            try
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
            catch (NullReferenceException)
            {
                return GetTodaysEndObj(DbMaster.GetUserLocale().LocaleData);
            }
        }

        public static DateTime GetWeekendCountdown(CountdownLocaleData cld)
        {
            try
            {
                DateTime now = DateTime.UtcNow;
                DateTime ret = now;
                switch (now.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        ret = now.AddDays(4);
                        break;
                    case DayOfWeek.Tuesday:
                        ret = now.AddDays(3);
                        break;
                    case DayOfWeek.Wednesday:
                        ret = now.AddDays(2);
                        break;
                    case DayOfWeek.Thursday:
                        ret = now.AddDays(1);
                        break;
                    case DayOfWeek.Saturday:
                        ret = now.AddDays(-1);
                        break;
                    case DayOfWeek.Sunday:
                        ret = now.AddDays(-2);
                        break;
                }
                return new DateTime(ret.Year, ret.Month, ret.Day, cld.FridayEnd.Hours, cld.FridayEnd.Minutes, 0);
                
            }
            catch (NullReferenceException)
            {
                return GetWeekendCountdown(DbMaster.GetUserLocale().LocaleData);
            }
        }
    }
}
