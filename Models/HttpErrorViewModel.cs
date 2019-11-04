using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class HttpErrorViewModel
    {
        public int StatusCode { get; set; }
        public string ErrorTitle { get; set; }
        public string ErrorDescription { get; set; }
    }
}
