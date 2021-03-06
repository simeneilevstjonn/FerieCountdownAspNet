﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class CustomLocaleWizardViewModel
    {
        public CustomLocaleWizardViewModel(string mode, string redirectUrl)
        {
            IsWork = mode switch
            {
                "school" => false,
                "work" => true,
                _ => throw new ArgumentException()
            };
            RedirectUrl = redirectUrl;
        }
        public bool IsWork = false;
        public string RedirectUrl { get; set; }
    }
}
