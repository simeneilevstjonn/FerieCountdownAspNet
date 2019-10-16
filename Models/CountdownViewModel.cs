using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class CountdownViewModel
    {
        
        public DateTime CountdownTime { get; set; }
        public string BackgroundPath { get; set; }
        public string CountdownText { get; set; }
        public string CountdownEndText { get; set; }
        public bool UseCCCText { get; set; }
        public string CssAppend { get; set; }
        public string HtmlAppend { get; set; }
        public bool UseLocalTime { get; set; }
    }
}
