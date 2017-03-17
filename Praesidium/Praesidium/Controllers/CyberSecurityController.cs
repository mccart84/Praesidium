using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Statistics()
        {
            return View();
        }

        public ActionResult LawEnforcement()
        {
            return View();
        }

        public ActionResult Quiz(int? quizId)
        {
            ViewBag.QuizId = quizId;
            return View();
        }
    }
}