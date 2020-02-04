using FerieCountdown.Classes.Locale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class IndexViewModel
    {
        public CountdownLocale Locale { get; set; }
        public bool HasLocale => Locale.Municipality != "error";
    }
}
