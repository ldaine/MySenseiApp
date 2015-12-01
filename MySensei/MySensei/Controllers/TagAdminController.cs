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

namespace MySensei.Controllers
{
    public class TagAdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: TagAdmin
        public ActionResult Index()
        {
            return View(db.AppTags.ToList());
        }

        // GET: TagAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTag appTag = db.AppTags.Find(id);
            if (appTag == null)
            {
                return HttpNotFound();
            }
            return View(appTag);
        }

        // GET: TagAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Tag")] AppTag appTag)
        {
            if (ModelState.IsValid)
            {
                db.AppTags.Add(appTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appTag);
        }

        // GET: TagAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTag appTag = db.AppTags.Find(id);
            if (appTag == null)
            {
                return HttpNotFound();
            }
            return View(appTag);
        }

        // POST: TagAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Tag")] AppTag appTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appTag);
        }

        // GET: TagAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppTag appTag = db.AppTags.Find(id);
            if (appTag == null)
            {
                return HttpNotFound();
            }
            return View(appTag);
        }

        // POST: TagAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppTag appTag = db.AppTags.Find(id);
            db.AppTags.Remove(appTag);
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
