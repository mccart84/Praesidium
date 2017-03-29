using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praesidium.Data_Models.Admin;

namespace Praesidium.Controllers
{
    public class CyberSecurityController : Controller
    {
        // GET: CyberSecurity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Resources()
        {
            return View(GetFilesByCategory(18));
        }

        public ActionResult Adults()
        {
            return View(GetFilesByCategory(12));
        }

        public ActionResult Teachers()
        {
            return View(GetFilesByCategory(15));
        }

        public ActionResult Teen()
        {
            return View(GetFilesByCategory(13));
        }

        public ActionResult Youth()
        {
            return View(GetFilesByCategory(14));
        }

        public ActionResult Statistics()
        {
            return View(GetFilesByCategory(17));
        }

        public ActionResult LawEnforcement()
        {
            return View(GetFilesByCategory(16));
        }

        public ActionResult Quiz(int? quizId)
        {
            ViewBag.QuizId = quizId;
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