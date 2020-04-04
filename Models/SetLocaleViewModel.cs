using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class SetLocaleViewModel
    {
        public SetLocaleViewModel(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }
        public string RedirectUrl { get; set; }
    }
}
