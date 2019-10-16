using FerieCountdown.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class CountdownConfiguratorViewModel
    {
        public string Title { get; set; }
        public string Heading { get; set; }
        public List<FormInput> Inputs { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
