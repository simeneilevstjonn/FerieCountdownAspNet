using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Io
{
    public class ResetPasswordEmail : EmailTemplate
    {
        public ResetPasswordEmail()
        {
            Subject = "Tilbakestill ditt passord.";
        }
        public string ConfirmUrl { get; set; }
        public override string Body
        {
            get => string.Format("Det har blitt forespurt en tilbakestilling av passord på din FerieCountdown-bruker. Dersom du ikke har gjort dette, kan du se bort fra denne e-posten. <br><br><div style=\"text-align: center;width:100%\"> <a href=\"{0}\" width=\"50%\" height=\"48px;\" style=\"text-decoration: none;display:inline-block;font-weight:400;text-align:center;vertical-align:middle;cursor:pointer;-webkit-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none;background-color:transparent;border:1px solid transparent;padding:5.25pt 10.5pt;line-height:48px;border-radius:3.5pt;color:#fff;background-color:#007bff;border-color:#007bff;\"> <span style=\"font-family: myriad-pro; font-size: 18pt; height: 100%; vertical-align: middle;\">Tilbakestill ditt passord</span> </a> </div><br>Eller klikk på linken: <a href=\"{0}\" style=\"color: black; font-size: 10pt\">{0}</a>",
                ConfirmUrl);
        }
        public override Dictionary<string, string> RightFooterData
        {
            get => new Dictionary<string, string>
            {
                { "Tid",  DateTime.UtcNow.ToString("u")},
                { "Bruker-IP",UserIP},
                { "Land",UserCountryCode}
            };
        }

        public string UserIP { get; set; }
        public string UserCountryCode { get; set; }
    }
}