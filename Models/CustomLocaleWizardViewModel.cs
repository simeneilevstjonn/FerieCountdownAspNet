using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class CustomLocaleWizardViewModel
    {
        public CustomLocaleWizardViewModel(string mode)
        {
            IsWork = mode switch
            {
                "school" => false,
                "work" => true,
                _ => throw new ArgumentException()
            };
        }
        public bool IsWork = false;
    }
}
