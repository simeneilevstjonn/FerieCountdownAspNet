using FerieCountdown.Classes;
using FerieCountdown.Classes.Countdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class CustomBuilderViewModel
    {
        public string Title { get; set; }
        public string Heading { get; set; }
        public List<CountdownBackground> BackgroundOptions { get; set; }
        public List<FormInput> FormInputs { get; set; }
        public string Action { get; set; }
    }
}
