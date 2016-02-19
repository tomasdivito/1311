using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace REST_magic1311.Models
{
    public class DownloadResult : ActionResult
    {
        public DownloadResult()
        {
        }

        public DownloadResult(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }

        public string VirtualPath
        {
            get;
            set;
        }

        public string FileDownloadName
        {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (!String.IsNullOrEmpty(FileDownloadName))
            {
                context.HttpContext.Response.AddHeader("content-disposition",
                  "attachment; filename=" + this.FileDownloadName);
            }

            string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);
            context.HttpContext.Response.TransmitFile(filePath);
        }
    }
}