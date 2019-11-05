using FerieCountdownWithAuth;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Io
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage
            {
                Subject = subject,
                From = new MailAddress("noreply@feriecountdown.com"),
                To = { new MailAddress(email) },
                IsBodyHtml = true,
                BodyEncoding = System.Text.Encoding.UTF8,
                Body = htmlMessage
            };

            SmtpClient client = new SmtpClient
            {
                Host = "ipsum.trok.no",
                Port = 587,
                Credentials = new System.Net.NetworkCredential(Environment.GetEnvironmentVariable("SMTP_USERNAME"), Environment.GetEnvironmentVariable("SMTP_PASSWORD")),
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
            await client.SendMailAsync(message);
            client.Dispose();
        }
    }
}
