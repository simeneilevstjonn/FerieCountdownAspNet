using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Io
{
    public class ContactFormEmail : EmailTemplate
    {
        public ContactFormEmail()
        {
            Body = new string(string.Format("<table> <tr> <td>Fra</td><td>{0}, {1}</td></tr><tr> <td>Sendt</td><td>{2}</td></tr><tr> <td colspan=\"2\"> Melding </td></tr><tr> <td colspan=\"2\">{3}</td></tr></table>",ReplyTo.DisplayName, ReplyTo.Address, DateTime.UtcNow.ToString("u"), Message));
            RightFooterData = new Dictionary<string, string> 
            {
                { "Bruker-IP",UserIP},
                { "Land",UserCountryCode}
            };
        }
        public string Message { get; set; }
        public string UserIP { get; set; }
        public string UserCountryCode { get; set; }
    }
}
