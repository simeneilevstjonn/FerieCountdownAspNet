using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FerieCountdown.Classes.Io
{
    public static class IoMaster
    {
        public static string GRCSecret { private get; set; } 
        public static void SendMail(MailMessage Message)
        {
            SmtpClient client = new SmtpClient
            {
                Host = "ipsum.trok.no",
                Port = 25,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };
            client.Send(Message);
            client.Dispose();
        }

        

        public static async Task<string> HttpPostRequest(Dictionary<string, string> formdata, string uri)
        {
            HttpClient client = new HttpClient();

            FormUrlEncodedContent content = new FormUrlEncodedContent(formdata);
            HttpResponseMessage response = await client.PostAsync(uri, content);
            client.Dispose();

            return await response.Content.ReadAsStringAsync();
        }

        public static bool VerifyRecaptcha(string userkey, string remoteip)
        {
            //ReCAPTCHA validation
            string httpresponse = HttpPostRequest(new Dictionary<string, string>
            {
                { "secret", GRCSecret },
                { "response", userkey },
                { "remoteip", remoteip }
            },
                "https://www.google.com/recaptcha/api/siteverify"
            ).Result;

            JObject recaptcharesponse = JObject.Parse(httpresponse);
            if (string.IsNullOrEmpty(recaptcharesponse["success"].ToString()) || string.IsNullOrEmpty(recaptcharesponse["action"].ToString())) return false;
            else if (recaptcharesponse["success"].ToString() == "false" || recaptcharesponse["action"].ToString() != "contact") return false;
            else return true;
        }
}
}
