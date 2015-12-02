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
    public class CourseAdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Admin/CourseAdmin
        public ActionResult Index()
        {
            var courses = db.Courses.Include(a => a.AppCategory).Include(a => a.AppCourseStatus).Include(a => a.AppUser);
            return View(courses.ToList());
        }

        // GET: Admin/CourseAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCourse appCourse = db.Courses.Find(id);
            if (appCourse == null)
            {
                return HttpNotFound();
            }
            return View(appCourse);
        }

        // GET: Admin/CourseAdmin/Create
        public ActionResult Create()
        {
            var teacher = db.Roles.Where(x => x.Name == "Teacher").FirstOrDefault().Id;
            var users = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(teacher));
            ViewBag.AppCategoryID = new SelectList(db.AppCategorys, "ID", "Category");
            ViewBag.AppCourseStatusID = new SelectList(db.AppCourseStatuss, "ID", "Status");
            ViewBag.AppUserID = new SelectList(users, "Id", "FullName");
            return View();
        }

        // POST: Admin/CourseAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CreateCourseModel model, string password)
        {
            AppCourse appCourse = new AppCourse();
            if (ModelState.IsValid)
            {
                appCourse.AppUserID = model.AppUserID;
                appCourse.AppCategoryID = model.AppCategoryID;
                appCourse.AppCourseStatusID = db.AppCourseStatuss.Where(x => x.Status == "New").FirstOrDefault().ID;
                appCourse.Course = model.Course;
                appCourse.Headline = model.Headline;
                appCourse.Description = model.Description;
                appCourse.CourseImage = model.CourseImage;
                appCourse.CreatedAt = DateTime.Now;
                appCourse.Location = model.Location;
                appCourse.Price = model.Price;
                appCourse.Rating = 0;
                appCourse.MaxAttendance = model.MaxAttendance;


                db.Courses.Add(appCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppCategoryID = new SelectList(db.AppCategorys, "ID", "Category", appCourse.AppCategoryID);
            ViewBag.AppCourseStatusID = new SelectList(db.AppCourseStatuss, "ID", "Status", appCourse.AppCourseStatusID);
            ViewBag.AppUserID = new SelectList(db.Users, "Id", "FirstName", appCourse.AppUserID);
            return View(model);
        }

        // GET: Admin/CourseAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCourse appCourse = db.Courses.Find(id);
            if (appCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppCategoryID = new SelectList(db.AppCategorys, "ID", "Category", appCourse.AppCategoryID);
            ViewBag.AppCourseStatusID = new SelectList(db.AppCourseStatuss, "ID", "Status", appCourse.AppCourseStatusID);
            ViewBag.AppUserID = new SelectList(db.Users, "Id", "FirstName", appCourse.AppUserID);
            return View(appCourse);
        }

        // POST: Admin/CourseAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AppUserID,AppCategoryID,AppCourseStatusID,Course,Headline,Description,CourseImage,CreatedAt,Location,Price,Rating,MaxAttendance")] AppCourse appCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppCategoryID = new SelectList(db.AppCategorys, "ID", "Category", appCourse.AppCategoryID);
            ViewBag.AppCourseStatusID = new SelectList(db.AppCourseStatuss, "ID", "Status", appCourse.AppCourseStatusID);
            ViewBag.AppUserID = new SelectList(db.Users, "Id", "FirstName", appCourse.AppUserID);
            return View(appCourse);
        }

        // GET: Admin/CourseAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppCourse appCourse = db.Courses.Find(id);
            if (appCourse == null)
            {
                return HttpNotFound();
            }
            return View(appCourse);
        }

        // POST: Admin/CourseAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppCourse appCourse = db.Courses.Find(id);
            db.Courses.Remove(appCourse);
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
