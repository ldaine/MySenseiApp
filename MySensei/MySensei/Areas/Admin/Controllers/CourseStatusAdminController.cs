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
    public class CourseStatusAdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Admin/CourseStatusAdmin
        public ActionResult Index()
        {
            return View(db.AppCourseStatuss.ToList());
        }

        // GET: Admin/CourseStatusAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCourseStatus appCourseStatus = db.AppCourseStatuss.Find(id);
            if (appCourseStatus == null)
            {
                return HttpNotFound();
            }
            return View(appCourseStatus);
        }

        // GET: Admin/CourseStatusAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CourseStatusAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Status")] AppCourseStatus appCourseStatus)
        {
            if (ModelState.IsValid)
            {
                db.AppCourseStatuss.Add(appCourseStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appCourseStatus);
        }

        // GET: Admin/CourseStatusAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCourseStatus appCourseStatus = db.AppCourseStatuss.Find(id);
            if (appCourseStatus == null)
            {
                return HttpNotFound();
            }
            return View(appCourseStatus);
        }

        // POST: Admin/CourseStatusAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Status")] AppCourseStatus appCourseStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appCourseStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appCourseStatus);
        }

        // GET: Admin/CourseStatusAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCourseStatus appCourseStatus = db.AppCourseStatuss.Find(id);
            if (appCourseStatus == null)
            {
                return HttpNotFound();
            }
            return View(appCourseStatus);
        }

        // POST: Admin/CourseStatusAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppCourseStatus appCourseStatus = db.AppCourseStatuss.Find(id);
            db.AppCourseStatuss.Remove(appCourseStatus);
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
