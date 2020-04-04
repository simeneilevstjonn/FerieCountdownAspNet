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

            await SendEmailAsync(message);
        }

        public async Task SendEmailAsync(MailMessage message)
        {
            SmtpClient client = new SmtpClient
            {
                Host = "localhost",
                Port = 25,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = false
            };
            await client.SendMailAsync(message);
            client.Dispose();
        }
    }
}
