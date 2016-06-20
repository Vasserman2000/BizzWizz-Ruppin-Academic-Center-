using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BizWizProj.Models
{
    public class MessageServices
    {
        public async static Task SendEmailAsync (string email, string subject, string message)
        {
            try
            {
                var _email = "bizzwizz2017@gmail.com";
                var _epass = ConfigurationManager.AppSettings["EmailPassword"];
                var _dispName = "BizzWizz";
                /*smtp*/
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.EnableSsl = true; // Secure Sockets Layer (standard security technology)
                    smtp.Host = "smtp.live.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); }; //release all the resources 
                                                                         //used by the current SmtpClient
                                                                         //and end the TCP Connection
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}