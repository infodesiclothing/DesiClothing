using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DesiClothing4u.Common.Models
{
    public class Email
    {
        public void Send(string email, string subject, string body, string bcs)
        {
            MailMessage message = new MailMessage();
            /*message.From = new MailAddress("info@desiclothingonline.com");*/
            message.From = new MailAddress("infodesiclothingonline@gmail.com");
            message.To.Add(email);
            message.Bcc.Add("info@desiclothingonline.com");
            message.Subject = subject;

            message.Body = body;
            message.IsBodyHtml = true;
            var client = new SmtpClient
            {
                Host = "win10.tmd.cloud",
                Port = 465,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(message.From.ToString(), "Need4speed!@")
            };
            client.Send(message);
            //string SMTP = "";
            //string BCC = "";
            //string EmailFrom = "";
            //string Password = "";
            //string host = HttpContext.Current.Request.Url.Host;
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient(SMTP.ToString());
            //mail.From = new MailAddress(EmailFrom.ToString());
            //mail.To.Add(email);
            //mail.Bcc.Add(BCC.ToString());
            //StreamReader reader = new StreamReader(path);
            //string readFile = reader.ReadToEnd();
            //string myString = "";
            //myString = readFile;
            //myString = myString.Replace("{#memberid}", id.ToString());
            //myString = myString.Replace("{#URL}", host.ToString());
            //mail.Subject = "Recover Password" + " " + DateTime.Now.ToString("dd/MMM/yyyy hh:mm:ss tt");
            //mail.IsBodyHtml = true;
            //mail.Body = myString.ToString();
            //SmtpServer.Port = 25;
            //SmtpServer.Credentials = new System.Net.NetworkCredential(EmailFrom.ToString(), Password.ToString());
            //SmtpServer.EnableSsl = false;
            //object userState = mail;
            //SmtpServer.SendCompleted += new SendCompletedEventHandler(SMTPClientForAsy.SmtpClient_OnCompleted);
            //SmtpServer.SendAsync(mail, userState);
        }
    }
}
