using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes
{
    public class CopyrightItem
    {
        public string Name { get; set; }
        public string Liscense { get; set; }
        public string CopyrightHolder { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }

        public CopyrightItem(string Name, string Liscense, string CopyrightHolder, string Link, string Image)
        {
            this.Name = Name;
            this.Liscense = Liscense;
            this.CopyrightHolder = CopyrightHolder;
            this.Link = Link;
            this.Image = Image;
        }
    }
}
