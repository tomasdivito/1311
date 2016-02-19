using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using REST_magic1311.Models;
using BotDetect.Web.UI.Mvc;
using System.Net.Mail;

namespace REST_magic1311.Controllers
{
    public class UserController : Controller
    {
        private Db_User_Validator duv;

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserModel user)
        {
            //if(ModelState.IsValid)
            //{
                if(IsValid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Home");
            }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect.");
                }
            //}
            return View(user);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Registration(UserModel user)
        {
            duv = new Db_User_Validator();
            if (ModelState.IsValid)
            {
                if (duv.CreateNewUser(user))
                {
                    //return RedirectToAction("Index", "Home");
                    return View("View", user);
                }
                else
                {
                    ModelState.AddModelError("", "User Already exists!");
                    return View(user);
                }
            }

            return View(user);
        }


        /*testing validation of the user creating
        [HttpGet]
        public ActionResult Validation(UserModel user)
        {
            return View(user);
        }

        [HttpPost]
        [ActionName("Validation")]
        public ActionResult ValidationPost(UserModel usr)
        {
            if(usr.ActivationCode == CodeValidator.Code)
            {
                if (duv.CreateNewUser(usr))
                {
                    //return RedirectToAction("Index", "Home");
                    return View("View", usr                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      );
                }
            }
            return View();
        }*/

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string email, string password)
        {
            SimpleCrypto.PBKDF2 crypto = new SimpleCrypto.PBKDF2();
            duv = new Db_User_Validator();
            bool isValid = false;

            UserModel user = duv.GetUser(email);
            if(user != null)
            {
                if(user.Password == crypto.Compute(password, user.PasswordSalt))
                {
                    isValid = true;
                }
            }

            return isValid;
        }


        private bool SendVerificationMail(string email, string key)
        {
            try
            {
                MailMessage mail = new MailMessage("validation@magic1311.com", email);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "mail.magic1311.com";
                client.Credentials = new System.Net.NetworkCredential("validation@magic1311.com", "v@l1d@t1i0n");
                mail.Subject = "Verification code";
                mail.Body = "This is you key to activate your account \n \n" + key;
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}