using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerieCountdown.Classes.TimeHandler;

namespace FerieCountdown.Classes.Locale
{
    public class RedTierData
    {
        public CountdownLocaleData Always { get; set; }
        public CountdownLocaleData EvenWeek { get; set; }
        public CountdownLocaleData OddWeek { get; set; }
        public CountdownLocaleData GetApplicableLocale 
        {
            get
            {
                // Return always if not null
                if (Always != null) return Always;

                // Find the current week number and return the applicable locale
                return (TimeMaster.GetIso8601WeekOfYear(DateTime.UtcNow) % 2 == 0) switch
                {
                    true => EvenWeek,
                    false => OddWeek
                };
            } 
        }
    }
}
