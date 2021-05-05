using FerieCountdown.Classes.LessonCounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static FerieCountdown.Classes.LessonCounter.Subjects;

namespace FerieCountdown.Models
{
    public class LessonCounterViewModel
    {
        public Dictionary<Subject, int> RemainingCount { get; set; }
        public Dictionary<string, int> RemainingCountStrings
        {
            get
            {
                // Define the return dict
                Dictionary<string, int> Count = new Dictionary<string, int>();

                // Iterate through dict
                foreach ((Subject s, int c) in RemainingCount)
                {
                    Count.Add(ToNbNoString(s), c);
                }

                // Return dict
                return Count;
            }
        }
    }
}
