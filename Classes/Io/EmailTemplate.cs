using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Io
{
    public class EmailTemplate
    {
        public virtual MailAddress ToEmail { get; set; }
        public virtual MailAddress ReplyTo { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Heading { get; set; }
        public virtual string FromName { get; set; }
        public virtual string Body { get; set; }
        public virtual Dictionary<string, string> RightFooterData { get; set; }

        public string EmailBody
        {
            get
            {
                string RawFooter = string.Empty;
                if (RightFooterData.Count > 0)
                {
                    bool notfirst = false;
                    foreach (KeyValuePair<string, string> kvp in RightFooterData)
                    {
                        if (notfirst) RawFooter += " - ";
                        else notfirst = true;
                        RawFooter += $"{kvp.Key}: {kvp.Value}";
                    }
                }

                return string.Format
                (
                    "<html> <head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <style>@import url(\"https://use.typekit.net/usk3mry.css\"); @media (max-width: 600px){{.no-mob{{display: none!important;}}.full-mob{{width: 100%!important;}}}}</style> </head> <body> <table style=\"height: 100%; width: 100%; margin:0\"> <tr height=\"10%\"></tr><tr> <td width=\"15%\" class=\"no-mob\"></td><td width=\"70%\" class=\"full-mob\" style=\"vertical-align: top; background-color: #ccc;\"> <table style=\"height: 100%; width: 100%\"> <tr height=\"64\"> <td width=\"100%\" colspan=\"3\" style=\"background-color: #333;\"></td></tr><tr height=\"32\"> <td width=\"64\"></td><td colspan=\"2\"> <img src=\"http://static.feriecountdown.com/resources/logo.png\" height=\"64\" width=\"64\"> </td></tr><tr> <td width=\"64\"></td><td width=\"100%\" style=\"vertical-align: top;font-family: myriad-pro; font-size: 14pt;\"> <br><h1 style=\"text-align: center;\">{0}</h1> <br><table style=\"height: 100%; width: 100%; margin:0\"> <tr> <td width=\"10%\"></td><td width=\"80%\" style=\"vertical-align: top;font-family: myriad-pro; font-size: 14pt;\"> <p>{1}</p></td><td width=\"10%\"></td></tr></table> </td><td width=\"64\" class=\"no-mob\"></td></tr><tr height=\"1\"> <td width=\"100%\" colspan=\"3\" style=\"background-color: #333;\"></td></tr><tr height=\"32\" style=\"vertical-align: bottom;font-family: myriad-pro; font-size: 12pt;\"> <td width=\"64\"></td><td width=\"100%\"> <table width=\"100%\"> <tr> <td> &copy; FerieCountdown 2020 - <a href=\"https://www.feriecountdown.com/Home/Contact\" style=\"color: black\">Kontakt oss</a> </td><td style=\"text-align: right;\">{2}</td></tr></table> </td><td width=\"64\"></td></tr><tr height=\"16\"></tr></table> </td><td width=\"15%\" class=\"no-mob\"></td></tr></table> </body></html>",
                    Heading ?? Subject, Body, RawFooter               
                );
            }
        }

        public static implicit operator MailMessage(EmailTemplate e)
        {
            return new MailMessage
            {
                Subject = e.Subject ?? e.Heading,
                From = new MailAddress("noreply@feriecountdown.com", e.FromName ?? "FerieCountdown"),
                IsBodyHtml = true,
                To = {e.ToEmail ?? throw new ArgumentNullException("Property ToEmail cannot be null.")},
                ReplyToList = { e.ReplyTo},
                BodyEncoding = System.Text.Encoding.UTF8,
                Body = e.EmailBody
            };
        }
    }
}
