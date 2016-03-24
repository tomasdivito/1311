using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace REST_magic1311.Models
{
    public class MailSender
    {
        public void SendMail(MailModel mail, string receiver)
        {
            try
            {
                MailMessage ml = new MailMessage("mailsender@magic1311.com", receiver);
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

        public void SendMail(MailModel mail, string receiver, string attach)
        {
            try
            {
                MailMessage ml = new MailMessage("mailsender@magic1311.com", receiver);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "mail.magic1311.com";
                client.Credentials = new System.Net.NetworkCredential("mailsender@magic1311.com", "m@ils3nd3r");
                ml.Subject = mail.Subject;
                ml.Body = mail.Content;
                string filePath = HttpContext.Current.Server.MapPath(attach);
                ml.Attachments.Add(new Attachment(filePath));
                client.Send(ml);
            }
            catch (Exception ex)
            {
            }
        }
    }
}