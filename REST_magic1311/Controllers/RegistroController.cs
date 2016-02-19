using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REST_magic1311.Models;
using System.Web.Security;
using System.Web.Hosting;

namespace REST_magic1311.Controllers
{
    public class RegistroController : Controller
    {
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegisterProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterProduct(SerialModel srl)
        {
            Db_Validator dv = new Db_Validator();

            if (ModelState.IsValid)
            {
                string bad_serial_statement = "'" + srl.Serial + "'" + " No es un serial válido";
                string user = User.Identity.Name;
                string serial_state_used_real = dv.SerialInUse(srl.Serial);
                bool correctID = dv.SerialCorrectAppID(srl.Serial, srl.AppID);
                bool registeredSerial = dv.RegisteredSerial(srl.Serial);

                if (serial_state_used_real != bad_serial_statement && serial_state_used_real != "True")
                    if (correctID)
                        if(!registeredSerial)
                        {
                            dv.RegisterSerial(srl.Serial, user);
                            //RedirectToAction("SuccessfullRegistration", "Registro");
                            return View("SuccesfullRegistration", srl);
                        }
            }
            ModelState.AddModelError("", "Invalid SERIAL and/or PRODUCT");
            return View(srl);
        }
    }
}