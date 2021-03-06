﻿using System;
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
            ToEmail = new MailAddress("simen@feriecountdown.com", "Simen Eilevstjønn");
        }
        public override string Body 
        {
            get => string.Format("<table> <tr> <td>Fra</td><td>{0}, {1}</td></tr><tr> <td>Sendt</td><td>{2}</td></tr><tr> <td colspan=\"2\"> Melding </td></tr><tr> <td colspan=\"2\">{3}</td></tr></table>", ReplyTo.DisplayName, ReplyTo.Address, DateTime.UtcNow.ToString("u"), Message);
        }
        public override Dictionary<string, string> RightFooterData
        {
            get => new Dictionary<string, string>
            {
                { "Bruker-IP",UserIP},
                { "Land",UserCountryCode}
            };
        }

        public override string FromName { get => ReplyTo.DisplayName; set => base.FromName = value; }
        public string Message { get; set; }
        public string UserIP { get; set; }
        public string UserCountryCode { get; set; }
    }
}
