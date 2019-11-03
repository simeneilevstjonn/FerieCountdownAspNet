using FerieCountdown.Classes.TimeHandler;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Locale
{
    public class CountdownLocale
    {
        public string Municipality { get; set; }
        public string School { get; set; }
        public CountdownLocaleData LocaleData { get; set; }
        public string Data { set
            {
                SetData(value);
            } }

        private void SetData(string data)
        {
            JObject ObjectData = JObject.Parse(data);
            try
            {
                LocaleData = new CountdownLocaleData
                {
                    MondayEnd = new Time((int)ObjectData["MondayEnd"]["Hours"], (int)ObjectData["MondayEnd"]["Minutes"]),
                    TuesdayEnd = new Time((int)ObjectData["TuesdayEnd"]["Hours"], (int)ObjectData["TuesdayEnd"]["Minutes"]),
                    WednesdayEnd = new Time((int)ObjectData["WednesdayEnd"]["Hours"], (int)ObjectData["WednesdayEnd"]["Minutes"]),
                    ThursdayEnd = new Time((int)ObjectData["ThursdayEnd"]["Hours"], (int)ObjectData["ThursdayEnd"]["Minutes"]),
                    FridayEnd = new Time((int)ObjectData["FridayEnd"]["Hours"], (int)ObjectData["FridayEnd"]["Minutes"]),
                    AutumnHoliday = (DateTime)ObjectData.GetValue("AutumnHoliday"),
                    ChristmasHoliday = (DateTime)ObjectData.GetValue("ChristmasHoliday"),
                    WinterHoliday = (DateTime)ObjectData.GetValue("WinterHoliday"),
                    EasterHoliday = (DateTime)ObjectData.GetValue("EasterHoliday"),
                    SummerHoliday = (DateTime)ObjectData.GetValue("SummerHoliday")
                };
            }
            catch { }
        }
    }
}
