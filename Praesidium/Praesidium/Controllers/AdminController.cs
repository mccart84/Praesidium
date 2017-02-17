using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Praesidium.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region [Navigation Items Admin]
        public ActionResult NavigationItems()
        {
            return View();
        }

        #endregion

        public ActionResult Sections()
        {
            return View();
        }

        #region [Resources Admin]
        public ActionResult Resources()
        {
            return View();
        }

        #endregion

        #region [Content Management Admin]
        public ActionResult ContentManagement()
        {
            return View();
        }

        #endregion
    }
}