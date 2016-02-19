using BotDetect.Web.UI.Mvc;
using REST_magic1311.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace REST_magic1311.Controllers
{
    public class SupportController : Controller
    {
        // GET: Support
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Index(MailModel mail)
        {
            if (ModelState.IsValid)
            {
                if (SendMail(mail))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(mail);
                }
            }
            return View(mail);
        }

        private bool SendMail(MailModel mail)
        {
            try
            {
                MailMessage ml = new MailMessage("mailsender@magic1311.com", "support@magic1311.com");
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "mail.magic1311.com";
                client.Credentials = new System.Net.NetworkCredential("mailsender@magic1311.com", "m@ils3nd3r");
                ml.Subject = "[" + User.Identity.Name + "] " + mail.Subject;
                ml.Body = mail.Content;
                client.Send(ml);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}