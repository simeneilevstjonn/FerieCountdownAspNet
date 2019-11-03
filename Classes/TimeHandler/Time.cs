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
        public void SetTime(int h, int m)
        {
            Minutes = m;
            Hours = h;
        }
    }
}
