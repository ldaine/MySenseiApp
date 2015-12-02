using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySensei.Infrastructure;
using MySensei.Models;

namespace MySensei.Areas.Admin.Controllers
{
    public class RoleAdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Admin/RoleAdmin
        public ActionResult Index()
        {
            return View(db.IdentityRoles.ToList());
        }

        // GET: Admin/RoleAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRole appRole = db.IdentityRoles.Find(id);
            if (appRole == null)
            {
                return HttpNotFound();
            }
            return View(appRole);
        }

        // GET: Admin/RoleAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/RoleAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                db.IdentityRoles.Add(appRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appRole);
        }

        // GET: Admin/RoleAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRole appRole = db.IdentityRoles.Find(id);
            if (appRole == null)
            {
                return HttpNotFound();
            }
            return View(appRole);
        }

        // POST: Admin/RoleAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appRole);
        }

        // GET: Admin/RoleAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRole appRole = db.IdentityRoles.Find(id);
            if (appRole == null)
            {
                return HttpNotFound();
            }
            return View(appRole);
        }

        // POST: Admin/RoleAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AppRole appRole = db.IdentityRoles.Find(id);
            db.IdentityRoles.Remove(appRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
