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
        public bool IsWork { get; set; }
        public bool UseRedTier { get; set; }
        public List<RedTierData> CohortData { get; private set; }
        public string RedTierData
        {
            set
            {
                // Parse JSON
                JArray Data = JArray.Parse(value);

                // Initialize CohortData
                CohortData = new List<RedTierData>();

                // Repeat for each cohort in array
                for (int i = 0; i < Data.Count; i++)
                {
                    // Define an instance of RedTierData
                    RedTierData rtd = new RedTierData();

                    // Check if Always exist within Cohort
                    try
                    {
                        if ((JObject)Data[i]["Always"] != null)
                        {
                            // Parse Always
                            rtd.Always = new CountdownLocaleData
                            {
                                MondayEnd = new Time((int)Data[i]["Always"]["MondayEnd"]["Hours"], (int)Data[i]["Always"]["MondayEnd"]["Minutes"]),
                                TuesdayEnd = new Time((int)Data[i]["Always"]["TuesdayEnd"]["Hours"], (int)Data[i]["Always"]["TuesdayEnd"]["Minutes"]),
                                WednesdayEnd = new Time((int)Data[i]["Always"]["WednesdayEnd"]["Hours"], (int)Data[i]["Always"]["WednesdayEnd"]["Minutes"]),
                                ThursdayEnd = new Time((int)Data[i]["Always"]["ThursdayEnd"]["Hours"], (int)Data[i]["Always"]["ThursdayEnd"]["Minutes"]),
                                FridayEnd = new Time((int)Data[i]["Always"]["FridayEnd"]["Hours"], (int)Data[i]["Always"]["FridayEnd"]["Minutes"]),
                            };
                        }
                    }
                    catch
                    {
                        // Parse OddWeek and EvenWeek
                        rtd.OddWeek = new CountdownLocaleData
                        {
                            MondayEnd = new Time((int)Data[i]["OddWeek"]["MondayEnd"]["Hours"], (int)Data[i]["OddWeek"]["MondayEnd"]["Minutes"]),
                            TuesdayEnd = new Time((int)Data[i]["OddWeek"]["TuesdayEnd"]["Hours"], (int)Data[i]["OddWeek"]["TuesdayEnd"]["Minutes"]),
                            WednesdayEnd = new Time((int)Data[i]["OddWeek"]["WednesdayEnd"]["Hours"], (int)Data[i]["OddWeek"]["WednesdayEnd"]["Minutes"]),
                            ThursdayEnd = new Time((int)Data[i]["OddWeek"]["ThursdayEnd"]["Hours"], (int)Data[i]["OddWeek"]["ThursdayEnd"]["Minutes"]),
                            FridayEnd = new Time((int)Data[i]["OddWeek"]["FridayEnd"]["Hours"], (int)Data[i]["OddWeek"]["FridayEnd"]["Minutes"]),
                        };

                        rtd.EvenWeek = new CountdownLocaleData
                        {
                            MondayEnd = new Time((int)Data[i]["EvenWeek"]["MondayEnd"]["Hours"], (int)Data[i]["EvenWeek"]["MondayEnd"]["Minutes"]),
                            TuesdayEnd = new Time((int)Data[i]["EvenWeek"]["TuesdayEnd"]["Hours"], (int)Data[i]["EvenWeek"]["TuesdayEnd"]["Minutes"]),
                            WednesdayEnd = new Time((int)Data[i]["EvenWeek"]["WednesdayEnd"]["Hours"], (int)Data[i]["EvenWeek"]["WednesdayEnd"]["Minutes"]),
                            ThursdayEnd = new Time((int)Data[i]["EvenWeek"]["ThursdayEnd"]["Hours"], (int)Data[i]["EvenWeek"]["ThursdayEnd"]["Minutes"]),
                            FridayEnd = new Time((int)Data[i]["EvenWeek"]["FridayEnd"]["Hours"], (int)Data[i]["EvenWeek"]["FridayEnd"]["Minutes"]),
                        };
                    }

                    // Add rtd to lsit
                    CohortData.Add(rtd);
                }
            }
        }
        public string Data 
        { 
            set
            {
                JObject ObjectData = JObject.Parse(value);
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
}
