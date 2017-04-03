using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using Praesidium.Data_Models.Admin;
using Praesidium.DAL;

namespace Praesidium.Controllers
{
    public class TraffickingController : Controller
    {
        // GET: Trafficking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Resources()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }

        public ActionResult Adults()
        {
            
            return View();
        }

        public ActionResult Teachers()
        {
            return View();
        }

        public ActionResult Teen()
        {
            return View();
        }

        public ActionResult Youth()
        {
            return View();
        }

        public ActionResult WarningSigns()
        {
            return View();
        }

        public List<FileView> GetFilesByCategory(int catId)
        {
            AdminEntities db = new AdminEntities();
            var files = new List<FileView>();
            try
            {
                var catfiles = db.ShFileKeywords.Where(m => m.Keyword == catId.ToString());

                foreach (var t in catfiles)
                {
                    FileView file = db.FileViews.First(u => u.RecId == t.FkShFile);
                    files.Add(file);
                }

                return files.OrderBy(m => m.DownloadCount).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public PartialViewResult FileList(int id)
        {
            var fileList = GetFilesByCategory(id);
            return PartialView("~/Views/Shared/_FileItems.cshtml",fileList);
        }
    }
}