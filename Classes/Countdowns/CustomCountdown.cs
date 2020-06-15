using FerieCountdown.Classes.TimeHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Countdowns
{
    public class CustomCountdown
    {
        public string Id { get; set; }
        public string Owner { get; set; }
        public string CountdownType { get; set; }
        DateTime _CountdownTime { get; set; }
        public DateTime CountdownTime
        {
            get { return _CountdownTime; }
            set
            {
                _CountdownTime = CountdownType switch
                {
                    "birthday" => TimeMaster.ValiDate(value),
                    "custom-recurring" => TimeMaster.ValiDate(value),
                    "monthly" => TimeMaster.MonthlyRecuring(value),
                    "weekly" => TimeMaster.WeeklyRecuring(value),
                    _ => value
                };

            } 
        }
        public string CountdownText { get; set; }
        public string CountdownEndText { get; set; }
        public CountdownBackground Background { get; set; }
        public bool UseLocalTime { get; set; }
    }
}
