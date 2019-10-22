using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdownWithAuth.Classes
{
    public class CountdownLocale
    {
        public int Id { get; set; }
        public string Municipality { get; set; }
        public string School { get; set; }
        
        public CountdownLocaleData LocaleData { get; private set; }
        /*public string Data { get { return Data; } set
            {
                Data = value;
                ObjectData = JObject.Parse(value);
                try
                {
                    LocaleData = new CountdownLocaleData
                    {
                        MondayEnd = new Time((int)ObjectData.GetValue("MondayEnd.Minutes"), (int)ObjectData.GetValue("MondayEnd.Hours")),
                        TuesdayEnd = new Time((int)ObjectData.GetValue("TuesdayEnd.Minutes"), (int)ObjectData.GetValue("TuesdayEnd.Hours")),
                        WednesdayEnd = new Time((int)ObjectData.GetValue("WednesdayEnd.Minutes"), (int)ObjectData.GetValue("WednesdayEnd.Hours")),
                        ThursdayEnd = new Time((int)ObjectData.GetValue("ThursdayEnd.Minutes"), (int)ObjectData.GetValue("ThursdayEnd.Hours")),
                        FridayEnd = new Time((int)ObjectData.GetValue("FridayEnd.Minutes"), (int)ObjectData.GetValue("FridayEnd.Hours")),
                        AutumnHoliday = DateTime.Parse((string)ObjectData.GetValue("AutumnHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                        ChristmasHoliday = DateTime.Parse((string)ObjectData.GetValue("ChristmasHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                        WinterHoliday = DateTime.Parse((string)ObjectData.GetValue("WinterHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                        EasterHoliday = DateTime.Parse((string)ObjectData.GetValue("EasterHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                        SummerHoliday = DateTime.Parse((string)ObjectData.GetValue("SummerHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind)
                    };
                }
                catch (InvalidCastException e)
                {
                    throw new Exception("Exception in CountdownLocale: " + e);
                }


            }
        }*/
        public string Data { get { return Data; } set
            {
                Data = value;
                SetData();
            } }

        private void SetData()
        {
           //JObject ObjectData = JObject.Parse(Data);
            /*try
            {
                LocaleData = new CountdownLocaleData
                {
                    MondayEnd = new Time((int)ObjectData.GetValue("MondayEnd.Minutes"), (int)ObjectData.GetValue("MondayEnd.Hours")),
                    TuesdayEnd = new Time((int)ObjectData.GetValue("TuesdayEnd.Minutes"), (int)ObjectData.GetValue("TuesdayEnd.Hours")),
                    WednesdayEnd = new Time((int)ObjectData.GetValue("WednesdayEnd.Minutes"), (int)ObjectData.GetValue("WednesdayEnd.Hours")),
                    ThursdayEnd = new Time((int)ObjectData.GetValue("ThursdayEnd.Minutes"), (int)ObjectData.GetValue("ThursdayEnd.Hours")),
                    FridayEnd = new Time((int)ObjectData.GetValue("FridayEnd.Minutes"), (int)ObjectData.GetValue("FridayEnd.Hours")),
                    AutumnHoliday = DateTime.Parse((string)ObjectData.GetValue("AutumnHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                    ChristmasHoliday = DateTime.Parse((string)ObjectData.GetValue("ChristmasHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                    WinterHoliday = DateTime.Parse((string)ObjectData.GetValue("WinterHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                    EasterHoliday = DateTime.Parse((string)ObjectData.GetValue("EasterHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind),
                    SummerHoliday = DateTime.Parse((string)ObjectData.GetValue("SummerHoliday"), null, System.Globalization.DateTimeStyles.RoundtripKind)
                };
            }
            catch (InvalidCastException e)
            {
                throw new Exception("Exception in CountdownLocale: " + e);
            }*/
        }
    }
}
