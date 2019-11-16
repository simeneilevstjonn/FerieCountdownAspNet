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
        public bool UseCCC { get; set; }

        public static Dictionary<string, CountdownBackground> Backgrounds = new Dictionary<string, CountdownBackground>
        {
            //Adobe Stock Images
            {
                "christmasinterior",
                new CountdownBackground
                {
                    Id = "christmasinterior",
                    CommonName = "Julepyntet stue",
                    Path = "https://static.feriecountdown.com/resources/background/christmaslivingroom.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            {
                "birthdaycake",
                new CountdownBackground
                {
                    Id = "birthdaycake",
                    CommonName = "Bursdagskake",
                    Path = "https://static.feriecountdown.com/resources/background/birthday.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            {
                "christmastreeoutdoor",
                new CountdownBackground
                {
                    Id = "christmastreeoutdoor",
                    CommonName = "Utendørs juletre",
                    Path = "https://static.feriecountdown.com/resources/background/christmastreeoutdoor.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "christmastreeoutdoorsnow",
                new CountdownBackground
                {
                    Id = "christmastreeoutdoor",
                    CommonName = "Utendørs juletre med snøeffekt",
                    Path = "https://static.feriecountdown.com/resources/background/christmastreeoutdoor.jpg",
                    Css = ".snow-container{position:absolute;left:0;height:80%;width:100%;max-width:100%;top:0;overflow:hidden;z-index:2;pointer-events:none}.snow{display:block;position:absolute;z-index:2;top:0;right:0;bottom:0;left:0;pointer-events:none;-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0);-webkit-animation:snow linear infinite;animation:snow linear infinite}.snow.foreground{background-image:url(https://static.feriecountdown.com/resources/snow/snow-large.png);-webkit-animation-duration:15s;animation-duration:15s}.snow.foreground.layered{-webkit-animation-delay:7.5s;animation-delay:7.5s}.snow.middleground{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-medium.png);-webkit-animation-duration:20s;animation-duration:20s}.snow.middleground.layered{-webkit-animation-delay:10s;animation-delay:10s}.snow.background{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-small.png);-webkit-animation-duration:30s;animation-duration:30s}.snow.background.layered{-webkit-animation-delay:15s;animation-delay:15s}@-webkit-keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}@keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}",
                    Html = "<div class=\"snow-container\"><div class=\"snow foreground\"></div><div class=\"snow foreground layered\"></div><div class=\"snow middleground\"></div><div class=\"snow middleground layered\"></div><div class=\"snow background\"></div><div class=\"snow background layered\"></div></div>",
                    UseCCC = false
                }
            },
            {
                "classroom",
                new CountdownBackground
                {
                    Id = "classroom",
                    CommonName = "Klasserom",
                    Path = "https://static.feriecountdown.com/resources/background/classroom.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "easter",
                new CountdownBackground
                {
                    Id = "easter",
                    CommonName = "Påskehare og påskeegg",
                    Path = "https://static.feriecountdown.com/resources/background/easter.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "jackolantern",
                new CountdownBackground
                {
                    Id = "jackolantern",
                    CommonName = "Halloween gresskarlykt",
                    Path = "https://static.feriecountdown.com/resources/background/halloween.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            {
                "fireworks",
                new CountdownBackground
                {
                    Id = "fireworks",
                    CommonName = "Fyrverkeri",
                    Path = "https://static.feriecountdown.com/resources/background/fireworks.png",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            {
                "17may",
                new CountdownBackground
                {
                    Id = "17may",
                    CommonName = "17. Maitog i Oslo",
                    Path = "https://static.feriecountdown.com/resources/background/17may.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            {
                "abstractcolours",
                new CountdownBackground
                {
                    Id = "abstractcolours",
                    CommonName = "Abstrakte farger",
                    Path = "https://static.feriecountdown.com/resources/background/abstractcolours.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            {
                "office",
                new CountdownBackground
                {
                    Id = "office",
                    CommonName = "Kontor",
                    Path = "https://static.feriecountdown.com/resources/background/office.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            //Odd skjæveland Images
            {
                "autumncolours",
                new CountdownBackground
                {
                    Id = "autumncolours",
                    CommonName = "Høstfarger i Fjellet",
                    Path = "https://static.feriecountdown.com/resources/background/autumn.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = true
                }
            },
            {
                "snow",
                new CountdownBackground
                {
                    Id = "snow",
                    CommonName = "Snø",
                    Path = "https://static.feriecountdown.com/resources/background/winter.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "snowwithfx",
                new CountdownBackground
                {
                    Id = "snowwithfx",
                    CommonName = "Snø med snøeffekt",
                    Path = "https://static.feriecountdown.com/resources/background/winter.jpg",
                    Css = ".snow-container{position:absolute;left:0;height:80%;width:100%;max-width:100%;top:0;overflow:hidden;z-index:2;pointer-events:none}.snow{display:block;position:absolute;z-index:2;top:0;right:0;bottom:0;left:0;pointer-events:none;-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0);-webkit-animation:snow linear infinite;animation:snow linear infinite}.snow.foreground{background-image:url(https://static.feriecountdown.com/resources/snow/snow-large.png);-webkit-animation-duration:15s;animation-duration:15s}.snow.foreground.layered{-webkit-animation-delay:7.5s;animation-delay:7.5s}.snow.middleground{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-medium.png);-webkit-animation-duration:20s;animation-duration:20s}.snow.middleground.layered{-webkit-animation-delay:10s;animation-delay:10s}.snow.background{background-image:image-url(https://static.feriecountdown.com/resources/snow/snow-small.png);-webkit-animation-duration:30s;animation-duration:30s}.snow.background.layered{-webkit-animation-delay:15s;animation-delay:15s}@-webkit-keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}@keyframes snow{0%{-webkit-transform:translate3d(0,-100%,0);transform:translate3d(0,-100%,0)}100%{-webkit-transform:translate3d(15%,100%,0);transform:translate3d(15%,100%,0)}}",
                    Html = "<div class=\"snow-container\"><div class=\"snow foreground\"></div><div class=\"snow foreground layered\"></div><div class=\"snow middleground\"></div><div class=\"snow middleground layered\"></div><div class=\"snow background\"></div><div class=\"snow background layered\"></div></div>",
                    UseCCC = false
                }
            },
            {
                "beachboat",
                new CountdownBackground
                {
                    Id = "beachboat",
                    CommonName = "Båt på stranda",
                    Path = "https://static.feriecountdown.com/resources/background/beachboat.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            //Shutterstock Images
            {
                "blueleaves",
                new CountdownBackground
                {
                    Id = "blueleaves",
                    CommonName = "Blå blader",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/blueleaves.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "bridge",
                new CountdownBackground
                {
                    Id = "bridge",
                    CommonName = "Taiwan Friendship Bridge",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/bridge.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "echinops",
                new CountdownBackground
                {
                    Id = "echinops",
                    CommonName = "Kuletistler",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/echinops.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "florence",
                new CountdownBackground
                {
                    Id = "florence",
                    CommonName = "Firenze svart-hvit",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/florence.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "forestvalley",
                new CountdownBackground
                {
                    Id = "forestvalley",
                    CommonName = "Dal med skog",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/forestvalley.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "indonesiangate",
                new CountdownBackground
                {
                    Id = "indonesiangate",
                    CommonName = "Indonesisk port",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/indonesiangate.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "maninpark",
                new CountdownBackground
                {
                    Id = "maninpark",
                    CommonName = "Mann ved tre i parken",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/maninpark.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "officevector",
                new CountdownBackground
                {
                    Id = "officevector",
                    CommonName = "Tegning av kontor/skrivepult",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/office.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "pineforest",
                new CountdownBackground
                {
                    Id = "pineforest",
                    CommonName = "Furuskog",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/pineforest.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "rose",
                new CountdownBackground
                {
                    Id = "rose",
                    CommonName = "Rose",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/rose.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "trainingwomanbeachphone",
                new CountdownBackground
                {
                    Id = "trainingwomanbeachphone",
                    CommonName = "Trenende dame med mobil på stranda",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/trainingwomanbeachphone.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "waterfall",
                new CountdownBackground
                {
                    Id = "waterfall",
                    CommonName = "Liten bekk med steiner",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/waterfall.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "womaninpark",
                new CountdownBackground
                {
                    Id = "womaninpark",
                    CommonName = "Dame med sekk i parken",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/womaninpark.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            },
            {
                "woodenboats",
                new CountdownBackground
                {
                    Id = "woodenboats",
                    CommonName = "Rød og blå trebåt",
                    Path = "https://static.feriecountdown.com/resources/background/shutterstock/woodenboats.jpg",
                    Css = string.Empty,
                    Html = string.Empty,
                    UseCCC = false
                }
            }
        };
    }
}
