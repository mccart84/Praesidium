using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Praesidium.Data_Models.Admin;

namespace Praesidium.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CISTeam()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DropDownPages()
        {
            ViewBag.Message = "Pages By Section.";

            return View();
        }

        public PartialViewResult NavigationItems(bool? IsAdmin)
        {
            var model = new Models.Navigation.NavigationModel();

            if (IsAdmin == true)
            {
                model.GetAdminNavItems();
            }
            else
            {
                model.GetNavItems();
            }
            
            return PartialView("~/Views/Shared/_Navigation.cshtml", model);
        }

        public PartialViewResult FileItems(string FileId, int n)
        {
            var db = new AdminEntities();
            List<ShFile> files = new List<ShFile>();
            foreach (var t in db.ShFileKeywords.Where(m => m.Keyword == FileId))
            {
                files.Add((ShFile)db.ShFiles.Where(a => a.RecId == t.FkShFile));
            }
            files = (List<ShFile>)files.OrderBy(m => m.DateUploaded);
            files = (List<ShFile>) files.Take(n);

            return PartialView("~/Views/Shared/_FileItems.cshtml",files);
        }
    }
}