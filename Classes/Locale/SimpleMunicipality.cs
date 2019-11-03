using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Locale
{
    public class SimpleMunicipality
    {
        public string Name { get; set; }
        public List<SimpleLocale> Schools = new List<SimpleLocale>();
    }
}
