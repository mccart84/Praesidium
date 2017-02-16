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

        public PartialViewResult UserItems()
        {
            var model = new Models.Users.UserModel();
            model.GetUserItems();
            return PartialView("~/Views/Shared/_AdminLayout.cshtml", model);
        }
    }
}