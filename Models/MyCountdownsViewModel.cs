using FerieCountdown.Classes.Countdowns;
using FerieCountdownWithAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FerieCountdown.Models
{
    public class MyCountdownsViewModel
    {
        public Dictionary<string, string> Countdowns { get; set; }

        public MyCountdownsViewModel(string UserID)
        {
            Countdowns = Startup._DbMaster.GetDictionaryFromSql($"SELECT Id, CountdownText FROM dbo.CustomCountdowns WHERE Owner = N'{UserID}';");
        }
    }
}
