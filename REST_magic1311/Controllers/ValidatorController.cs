using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REST_magic1311.Models;

namespace REST_magic1311.Controllers
{
    public class ValidatorController : Controller
    {
        // GET: Validator
        public ActionResult Index()
        {
            return View();
        }

        public string Register(string email, string appID)
        {
            Db_SoftwareActivation dsa = new Db_SoftwareActivation();
            string res;
            res = dsa.ActivateSoftware(email, appID);
            return res;
        }

        public string Activate(string serial, string appID)
        {
            Db_Validator dv = new Db_Validator();
            string serialInUse = dv.SerialInUse(serial);
            string wrongSerialStatement = "'" + serial + "'" + " No es un serial válido";
            

            if (serialInUse == "False")
            {
                if (dv.SerialCorrectAppID(serial, appID))
                {
                    if (dv.RegisteredSerial(serial))
                    {
                        if (dv.Activate(serial) == "True")
                        {
                            return "Activado";
                        }
                        else
                        {
                            return "Error al activar";
                        }
                    }
                    else
                    {
                        return "Serial no registrado";
                    }
                }
                else
                {
                    return "Serial incorrecto para esta aplicación";
                }
            }
            else if (serialInUse == "True")
            {
                return "Codigo en uso!";
            }

            else
            {
                return wrongSerialStatement;
            }
        }
    }
}