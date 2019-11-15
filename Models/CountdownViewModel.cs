using FerieCountdown.Classes.Countdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class CountdownViewModel
    {
        
        public DateTime CountdownTime { get; set; }
        public string CountdownText { get; set; }
        public string CountdownEndText { get; set; }
        public CountdownBackground Background { get; set; }
        public bool UseLocalTime { get; set; }
    }
}
