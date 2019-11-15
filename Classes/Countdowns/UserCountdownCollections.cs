using FerieCountdown.Classes.Io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Countdowns
{
    public class UserCountdownCollections
    {
        public Dictionary<string, string> BirthdayCountdowns { get; set; }
        public Dictionary<string, string> WeddingCountdowns { get; set; }
        public Dictionary<string, string> ConfirmationCountdows { get; set; }
        public Dictionary<string, string> CustomCountdown { get; set; }

        public static UserCountdownCollections GetUserCountdowns(string userid)
        {
            Dictionary<string, string> BirthdayCountdowns = DbMaster.GetDictionaryFromSql($"SELECT Id, CountdownText FROM dbo.CustomCountdowns WHERE Owner = N'{userid}' AND CountdownType = N'birthday';");
            Dictionary<string, string> WeddingCountdowns = DbMaster.GetDictionaryFromSql($"SELECT Id, CountdownText FROM dbo.CustomCountdowns WHERE Owner = N'{userid}' AND CountdownType = N'wedding';");
            Dictionary<string, string> ConfirmationCountdows = DbMaster.GetDictionaryFromSql($"SELECT Id, CountdownText FROM dbo.CustomCountdowns WHERE Owner = N'{userid}' AND CountdownType = N'Confirmation';");
            Dictionary<string, string> CustomCountdown = DbMaster.GetDictionaryFromSql($"SELECT Id, CountdownText FROM dbo.CustomCountdowns WHERE Owner = N'{userid}' AND (CountdownType = N'custom' OR CountdownType = N'custom-reccurring');");

            return new UserCountdownCollections
            {
                BirthdayCountdowns = BirthdayCountdowns,
                WeddingCountdowns = WeddingCountdowns,
                ConfirmationCountdows = ConfirmationCountdows,
                CustomCountdown = CustomCountdown
            };
        }
    }
}
