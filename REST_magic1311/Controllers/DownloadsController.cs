using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REST_magic1311.Models;

namespace REST_magic1311.Controllers
{
    public class DownloadsController : Controller
    {
        // GET: Downloads
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadESP()
        {
            return new DownloadResult { VirtualPath = "~/MyApps/ESP/ESP.apk", FileDownloadName = "ESP.apk" };
        }

        public ActionResult DownloadTRA()
        {
            return new DownloadResult { VirtualPath = "~/MyApps/TRA/TRA.apk", FileDownloadName = "TRA.apk" };
        }

        public ActionResult DownloadPET()
        {
            return new DownloadResult { VirtualPath = "~/MyApps/PET/PET.apk", FileDownloadName = "PET.apk" };
        }
        
        [Authorize]
        public ActionResult DownloadManual()
        {
            return new DownloadResult { VirtualPath = "~/Files/ESP_Ingles_Espa.pdf", FileDownloadName = "Effect manual.pdf" };
        }
    }
}