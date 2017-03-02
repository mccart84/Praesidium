using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Praesidium.Data_Models.Admin;
using PagedList;
using Praesidium.Models;


namespace Praesidium.Controllers
{
    public class AdminController : Controller
    {
        private AdminEntities db = new AdminEntities();

        public ActionResult Login()
        {
            return View();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        #region [Navigation Items Admin]
        public ActionResult NavigationItems(string sortOrder, int? page)
        {
            page = page == null ? 1 : page;

            ViewBag.SectionSortParm = sortOrder == "Section" ? "section_desc" : "Section";
            ViewBag.DisplayTextSortParm = sortOrder == "DisplayText" ? "displayText_desc" : "DisplayText";
            ViewBag.IsActiveSortParm = sortOrder == "IsActive" ? "isActive_desc" : "IsActive";

            var navItems = db.ShSyNavigationItems.Include(s => s.ShSySection);

            switch (sortOrder)
            {
                case "section_desc":
                    navItems = navItems.OrderByDescending(x => x.ShSySection.Name);
                    break;
                case "Section":
                    navItems = navItems.OrderBy(x => x.ShSySection.Name);
                    break;
                case "displayText_desc":
                    navItems = navItems.OrderByDescending(x => x.DisplayText);
                    break;
                case "DisplayText":
                    navItems = navItems.OrderBy(x => x.DisplayText);
                    break;
                case "isActive_desc":
                    navItems = navItems.OrderByDescending(x => x.IsActive);
                    break;
                case "IsActive":
                    navItems = navItems.OrderBy(x => x.IsActive);
                    break;
                default:
                    navItems = navItems.OrderBy(x => x.ShSySection.Name);
                    break;
            }

            var sections = db.ShSySections.ToList();

            ViewBag.Sections = sections;

            int pageSize = 9;
            int pageNumber = (page ?? 1);

            return View(navItems.ToPagedList(pageNumber, pageSize));
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

        public ActionResult NavigationItemsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShSyNavigationItem shSyNavigationItem = db.ShSyNavigationItems.Find(id);
            if (shSyNavigationItem == null)
            {
                return HttpNotFound();
            }
            return View(shSyNavigationItem);
        }

        public ActionResult NavigationItemsCreate()
        {
            ViewBag.FkShSySection = new SelectList(db.ShSySections, "RecId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NavigationItemsCreate([Bind(Include = "RecId,Controller,Action,DisplayText,IsActive,FkShSySection,SortOrder,ParentId")] ShSyNavigationItem shSyNavigationItem)
        {
            if (ModelState.IsValid)
            {
                db.ShSyNavigationItems.Add(shSyNavigationItem);
                db.SaveChanges();
                return RedirectToAction("NavigationItems");
            }

            ViewBag.FkShSySection = new SelectList(db.ShSySections, "RecId", "Name", shSyNavigationItem.FkShSySection);
            return View(shSyNavigationItem);
        }

        public ActionResult NavigationItemsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShSyNavigationItem shSyNavigationItem = db.ShSyNavigationItems.Find(id);
            if (shSyNavigationItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkShSySection = new SelectList(db.ShSySections, "RecId", "Name", shSyNavigationItem.FkShSySection);
            return View(shSyNavigationItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NavigationItemsEdit([Bind(Include = "RecId,Controller,Action,DisplayText,IsActive,FkShSySection,SortOrder,ParentId")] ShSyNavigationItem shSyNavigationItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shSyNavigationItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NavigationItems");
            }
            ViewBag.FkShSySection = new SelectList(db.ShSySections, "RecId", "Name", shSyNavigationItem.FkShSySection);
            return View(shSyNavigationItem);
        }

        public ActionResult NavigationItemsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShSyNavigationItem shSyNavigationItem = db.ShSyNavigationItems.Find(id);
            if (shSyNavigationItem == null)
            {
                return HttpNotFound();
            }
            return View(shSyNavigationItem);
        }

        [HttpPost, ActionName("NavigationItemsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult NavigationItemsDeleteConfirmed(int id)
        {
            ShSyNavigationItem shSyNavigationItem = db.ShSyNavigationItems.Find(id);
            db.ShSyNavigationItems.Remove(shSyNavigationItem);
            db.SaveChanges();
            return RedirectToAction("NavigationItems");
        }

        #endregion

        #region[Sections]
        public ActionResult Sections()
        {
            var sections = db.ShSySections.OrderBy(x => x.Name);
            return View(sections.ToList());
        }

        public ActionResult SectionsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShSySection shSySection = db.ShSySections.Find(id);
            if (shSySection == null)
            {
                return HttpNotFound();
            }
            return View(shSySection);
        }

        public ActionResult SectionsCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SectionsCreate([Bind(Include = "RecId,Name,IsActive")] ShSySection shSySection)
        {
            if (ModelState.IsValid)
            {
                db.ShSySections.Add(shSySection);
                db.SaveChanges();
                return RedirectToAction("Sections");
            }

            return View(shSySection);
        }

        public ActionResult SectionsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShSySection shSySection = db.ShSySections.Find(id);
            if (shSySection == null)
            {
                return HttpNotFound();
            }
            return View(shSySection);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SectionsEdit([Bind(Include = "RecId,Name,IsActive")] ShSySection shSySection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shSySection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Sections");
            }
            return View(shSySection);
        }

        public ActionResult SectionsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShSySection shSySection = db.ShSySections.Find(id);
            if (shSySection == null)
            {
                return HttpNotFound();
            }
            return View(shSySection);
        }

        [HttpPost, ActionName("SectionsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult SectionsDeleteConfirmed(int id)
        {
            ShSySection shSySection = db.ShSySections.Find(id);
            db.ShSySections.Remove(shSySection);
            db.SaveChanges();
            return RedirectToAction("Sections");
        }


        #endregion

        #region[Users]
        public ActionResult Users()
        {
            var users = db.ShUsers.OrderBy(x => x.Username);
            return View(users.ToList());
        }

        public ActionResult UsersDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShUser shUser = db.ShUsers.Find(id);
            if (shUser == null)
            {
                return HttpNotFound();
            }
            return View(shUser);
        }

        public ActionResult UsersCreate()
        {
            ViewBag.FkShUserType = new SelectList(db.ShUserTypes, "RecId", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersCreate([Bind(Include = "RecId,FirstName,LastName,Username,email,FkShUserType,UserCreatedBy,UserUpdatedBy,UserDeletedBy,DateCreated,DateUpdated,DateDeleted,IsDeleted,password")] ShUser shUser)
        {
            if (ModelState.IsValid)
            {
                db.ShUsers.Add(shUser);
                db.SaveChanges();
                return RedirectToAction("Users");
            }

            ViewBag.FkShUserType = new SelectList(db.ShUserTypes, "RecId", "Type", shUser.FkShUserType);
            return View(shUser);
        }

        public ActionResult UsersEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShUser shUser = db.ShUsers.Find(id);
            if (shUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.FkShUserType = new SelectList(db.ShUserTypes, "RecId", "Type", shUser.FkShUserType);
            return View(shUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsersEdit([Bind(Include = "RecId,FirstName,LastName,Username,email,FkShUserType,UserCreatedBy,UserUpdatedBy,UserDeletedBy,DateCreated,DateUpdated,DateDeleted,IsDeleted,password")] ShUser shUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Users");
            }
            ViewBag.FkShUserType = new SelectList(db.ShUserTypes, "RecId", "Type", shUser.FkShUserType);
            return View(shUser);
        }

        public ActionResult UsersDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShUser shUser = db.ShUsers.Find(id);
            if (shUser == null)
            {
                return HttpNotFound();
            }
            return View(shUser);
        }

        [HttpPost, ActionName("UsersDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UsersDeleteConfirmed(int id)
        {
            ShUser shUser = db.ShUsers.Find(id);
            db.ShUsers.Remove(shUser);
            db.SaveChanges();
            return RedirectToAction("Users");
        }
        #endregion

        #region[User Types]

        public ActionResult UserTypes()
        {
            return View(db.ShUserTypes.ToList());
        }

        public ActionResult UserTypesDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShUserType shUserType = db.ShUserTypes.Find(id);
            if (shUserType == null)
            {
                return HttpNotFound();
            }
            return View(shUserType);
        }

        public ActionResult UserTypesCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserTypesCreate([Bind(Include = "RecId,Type,UserCreatedBy,UserUpdatedBy,UserDeletedBy,DateCreated,DateUpdated,DateDeleted,IsDeleted")] ShUserType shUserType)
        {
            if (ModelState.IsValid)
            {
                db.ShUserTypes.Add(shUserType);
                db.SaveChanges();
                return RedirectToAction("UserTypes");
            }

            return View(shUserType);
        }

        public ActionResult UserTypesEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShUserType shUserType = db.ShUserTypes.Find(id);
            if (shUserType == null)
            {
                return HttpNotFound();
            }
            return View(shUserType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserTypesEdit([Bind(Include = "RecId,Type,UserCreatedBy,UserUpdatedBy,UserDeletedBy,DateCreated,DateUpdated,DateDeleted,IsDeleted")] ShUserType shUserType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shUserType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserTypes");
            }
            return View(shUserType);
        }

        public ActionResult UserTypesDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShUserType shUserType = db.ShUserTypes.Find(id);
            if (shUserType == null)
            {
                return HttpNotFound();
            }
            return View(shUserType);
        }

        [HttpPost, ActionName("UserTypesDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShUserType shUserType = db.ShUserTypes.Find(id);
            db.ShUserTypes.Remove(shUserType);
            db.SaveChanges();
            return RedirectToAction("UserTypes");
        }

        #endregion

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

            var filelist = db.ShFiles.Include(u => u.ShUser1);
            return View(filelist.ToList());
        }

        public ActionResult FilesCreate()
        {
            ViewBag.users = new SelectList(db.ShUsers, "RecID", "Username");
            ViewBag.FkShSySection = new SelectList(db.ShSySections.Where(m => (bool)m.IsActive), "RecID", "Name");
            ViewBag.navitems = db.ShSyNavigationItems.Where(m => m.FkShSySection == 2)
                .Where(m => m.FkShSySection == 3);
            return View();
        }

        [HttpPost]
        public ActionResult FilesCreate(ShFile model, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {

                        model.FileName = upload.FileName;
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            model.FileStore = reader.ReadBytes(upload.ContentLength);
                        }
                        model.ContentType = upload.ContentType;
                        model.DateUploaded = DateTime.Now;

                        db.ShFiles.Add(model);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Files");
                }
            }
            catch (Exception ex /* dex */)
            {
                throw ex;
            }

            return View();
        }

        public ActionResult FilesEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var file = db.ShFiles.Find(id);



            if (file == null)
                return HttpNotFound();

            ViewBag.section = new SelectList(db.ShSySections, "RecID", "Name", file.FkShSySection);
            ViewBag.uploadusers = file.UploadedBy != null ? new SelectList(db.ShUsers, "RecID", "Username", file.UploadedBy) : new SelectList(db.ShUsers, "RecID", "Username");
            ViewBag.modusers = file.ModifiedBy != null ? new SelectList(db.ShUsers, "RecID", "Username", file.ModifiedBy) : new SelectList(db.ShUsers, "RecID", "Username");
            return View(file);
        }

        [HttpPost]
        public ActionResult FilesEdit(ShFile model, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {

                        model.FileName = upload.FileName;
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            model.FileStore = reader.ReadBytes(upload.ContentLength);
                        }
                        model.ContentType = upload.ContentType;
                        model.DateUploaded = DateTime.Now;

                        db.ShFiles.Add(model);
                        db.SaveChanges();
                    }

                    return RedirectToAction("Files");
                }
            }
            catch (Exception ex /* dex */)
            {
                throw ex;
            }

            return View();
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}