using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Locale
{
    public class LocaleParser
    {
        public static CountdownLocale ParseCookieLocale(HttpRequest Request)
        {
            return new CountdownLocale
            {
                Data = Request.Cookies["CustomLocaleData"],
                IsWork = Request.Cookies["IsWork"] switch
                {
                    "1" => true,
                    _ => false
                }
            };
        }
    }
}
