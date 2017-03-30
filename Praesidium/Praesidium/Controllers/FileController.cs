using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praesidium.Data_Models.Admin;

namespace Praesidium.Controllers
{
    public class FileController : Controller
    {
        private AdminEntities db = new AdminEntities();
        // GET: Download
        public FileResult Download(int id)
        {
            ShFile file = db.ShFiles.Find(id);
            if (file != null)
            {
                file.DownloadCount++;
                db.SaveChanges();
                var newfile = new FileContentResult(file.FileStore, file.ContentType);
                newfile.FileDownloadName = file.FileName;
                return newfile;
            }

            else
            {
                throw new HttpException(404, "File Not Found");
            }

        }
        // GET: File
        public ActionResult Show(int id)
        {
            var file = db.FileViews.Find(id);
            if (file == null)
                throw new HttpException(404, "Invalid File Specified");
            else
                return View(file);
        }
    }
}