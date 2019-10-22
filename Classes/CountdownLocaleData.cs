using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdownWithAuth.Classes
{
    public class CountdownLocaleData
    {
        public Time MondayEnd { get; set; }
        public Time TuesdayEnd { get; set; }
        public Time WednesdayEnd { get; set; }
        public Time ThursdayEnd { get; set; }
        public Time FridayEnd { get; set; }
        public DateTime ChristmasHoliday { get; set; }
        public DateTime EasterHoliday { get; set; }
        public DateTime WinterHoliday { get; set; }
        public DateTime SummerHoliday { get; set; }
        public DateTime AutumnHoliday { get; set; }
    }
}
