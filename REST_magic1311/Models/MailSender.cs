using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace REST_magic1311.Models
{
    public class MailSender
    {
        public void SendMail(MailModel mail)
        {
            try
            {
                MailMessage ml = new MailMessage("mailsender@magic1311.com", "tomasdv2@gmail.com");
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "mail.magic1311.com";
                client.Credentials = new System.Net.NetworkCredential("mailsender@magic1311.com", "m@ils3nd3r");
                ml.Subject = mail.Subject;
                ml.Body = mail.Content;
                client.Send(ml);
            }
            catch (Exception ex)
            {
            }
        }
    }
}