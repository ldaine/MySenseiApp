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
    public class SignUpsAdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Admin/SignUpsAdmin
        public ActionResult Index()
        {
            var signUps = db.SignUps.Include(a => a.AppCourse).Include(a => a.AppUser);
            return View(signUps.ToList());
        }

        // GET: Admin/SignUpsAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppSignUp appSignUp = db.SignUps.Find(id);
            if (appSignUp == null)
            {
                return HttpNotFound();
            }
            return View(appSignUp);
        }

        // GET: Admin/SignUpsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.AppCourseID = new SelectList(db.Courses, "ID", "AppUserID");
            ViewBag.AppUserID = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Admin/SignUpsAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AppUserID,AppCourseID,SignUpDate,feedback")] AppSignUp appSignUp)
        {
            if (ModelState.IsValid)
            {
                db.SignUps.Add(appSignUp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppCourseID = new SelectList(db.Courses, "ID", "AppUserID", appSignUp.AppCourseID);
            ViewBag.AppUserID = new SelectList(db.Users, "Id", "FirstName", appSignUp.AppUserID);
            return View(appSignUp);
        }

        // GET: Admin/SignUpsAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppSignUp appSignUp = db.SignUps.Find(id);
            if (appSignUp == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppCourseID = new SelectList(db.Courses, "ID", "AppUserID", appSignUp.AppCourseID);
            ViewBag.AppUserID = new SelectList(db.Users, "Id", "FirstName", appSignUp.AppUserID);
            return View(appSignUp);
        }

        // POST: Admin/SignUpsAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AppUserID,AppCourseID,SignUpDate,feedback")] AppSignUp appSignUp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appSignUp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppCourseID = new SelectList(db.Courses, "ID", "AppUserID", appSignUp.AppCourseID);
            ViewBag.AppUserID = new SelectList(db.Users, "Id", "FirstName", appSignUp.AppUserID);
            return View(appSignUp);
        }

        // GET: Admin/SignUpsAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppSignUp appSignUp = db.SignUps.Find(id);
            if (appSignUp == null)
            {
                return HttpNotFound();
            }
            return View(appSignUp);
        }

        // POST: Admin/SignUpsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppSignUp appSignUp = db.SignUps.Find(id);
            db.SignUps.Remove(appSignUp);
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
