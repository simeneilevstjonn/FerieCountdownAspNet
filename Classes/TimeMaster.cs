using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes
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
    }
}
