using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praesidium.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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

        public PartialViewResult NavigationItems()
        {
            var model = new Models.Navigation.NavigationModel();
            model.GetNavItems();
            return PartialView("~/Views/Shared/_Navigation.cshtml", model);
        }
    }
}