using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.TimeHandler
{
    public class Time
    {
        public int Minutes { get; private set; }
        public int Hours { get; private set; }
        public Time(int h, int m)
        {
            Minutes = m;
            Hours = h;
        }
        
        public Time(string time)
        {
            Hours = int.Parse(time.Substring(0, 2));
            Minutes = int.Parse(time.Substring(3, 2));
        }

        public void SetTime(int h, int m)
        {
            Minutes = m;
            Hours = h;
        }
    }
}
