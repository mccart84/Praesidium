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
            return View(GetFilesByCategory(11));
        }

        public ActionResult Statistics()
        {
            return View(GetFilesByCategory(10));
        }

        public ActionResult Adults()
        {
            
            return View(GetFilesByCategory(6));
        }

        public ActionResult Teachers()
        {
            return View(GetFilesByCategory(9));
        }

        public ActionResult Teen()
        {
            return View(GetFilesByCategory(7));
        }

        public ActionResult Youth()
        {
            return View(GetFilesByCategory(8));
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

                return files;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}