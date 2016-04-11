using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REST_magic1311.Source;

namespace REST_magic1311.Controllers
{
    public class CardGameController : Controller
    {
        // GET: CardGame
        public ActionResult Index()
        {
            return View();
        }

        public string GetGameByID(string ID)
        {
            return "";
        }
    }
}