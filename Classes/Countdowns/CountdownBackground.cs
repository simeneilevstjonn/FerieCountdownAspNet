using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Countdowns
{
    public class CountdownBackground
    {
        public string Id { get; set; }
        public string CommonName { get; set; }
        public string Path { get; set; }
        public string Css { get; set; }
        public string Html { get; set; }

        public static Dictionary<string, CountdownBackground> Backgrounds = new Dictionary<string, CountdownBackground> 
        {
            { 
                "birthdaycake",
                new CountdownBackground
                {
                    Id = "birthdaycake",
                    CommonName = "Bursdagskake",
                    Path = "https://static.feriecountdown.com/resources/background/bd/static.jpg",
                    Css = string.Empty,
                    Html = string.Empty
                }
            },
            {
                "fireworks",
                new CountdownBackground
                {
                    Id = "fireworks",
                    CommonName = "Fyrverkeri",
                    Path = "https://static.feriecountdown.com/resources/background/ny/static.png",
                    Css = string.Empty,
                    Html = string.Empty
                }
            }
        };
    }
}
