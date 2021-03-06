﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REST_magic1311.Models;

namespace REST_magic1311.Controllers
{
    public class MyAppsController : Controller
    {
        // GET: MyApps
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyRegisteredApps()
        {
            Db_Validator dv = new Db_Validator();

            List<SerialModel> seriales = dv.GetSerialesFromUser(User.Identity.Name);
            ViewBag.Seriales = seriales;
            return View();
        }
    }
}