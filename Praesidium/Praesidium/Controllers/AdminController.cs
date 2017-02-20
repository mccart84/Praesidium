using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Praesidium.Data_Models.Admin;
namespace Praesidium.Controllers
{
    public class AdminController : Controller
    {
        private AdminEntities db = new AdminEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region [Navigation Items Admin]
        public ActionResult NavigationItems()
        {
            var shSyNavigationItems = db.ShSyNavigationItems.Include(s => s.ShSySection).OrderBy(x => x.Controller);
            var sections = db.ShSySections.ToList();

            ViewBag.Sections = sections;

            return View(shSyNavigationItems.ToList());
        }

        public JsonResult GetSelectedRecord(int? id)
        {
            var selectedItem = db.ShSyNavigationItems.Select(x => new
            {
                x.Controller,
                x.Action,
                x.DisplayText,
                x.IsActive,
                x.FkShSySection,
                x.ShSySection.Name,
                x.RecId,
                x.ParentId
            }).ToList();
            return Json(selectedItem);
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


        // Addition for Site Documentation

        #region [Site Documentation Admin]
        public ActionResult Site_Documentation()
        {
            return View();
        }

        #endregion

        #region [Files Admin]
        public ActionResult Files()
        {
            var filelist = db.ShFiles;
            return View(filelist.ToList());
        }

        public ActionResult AddFile()
        {
            ViewBag.SectionList = new SelectList(db.ShSySections, "RecID", "Name");
            return View();
        }


        #endregion
    }
}