using Microsoft.AspNet.Identity;
using MySensei.Infrastructure;
using MySensei.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MySensei.Controllers
{
    public class CourseController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

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
        public ActionResult SignUpForCourse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Create a sign up object
            AppSignUp appSignUp = new AppSignUp();
            appSignUp.AppUserID = User.Identity.GetUserId();
            if (id != null) { appSignUp.AppCourseID = id.GetValueOrDefault(); }
            appSignUp.SignUpDate = DateTime.Now;

            AppSignUp NewAppSignUp = db.SignUps.Add(appSignUp);
            //get the user and course
            AppCourse appCourse = db.Courses.Find(id);
            AppUser appUser = db.Users.Find(User.Identity.GetUserId());

            //check if the values are not null
            if (appCourse == null)
            {
                return HttpNotFound();
            }

            if (appUser == null)
            {
                return HttpNotFound();
            }

            //add signup to course and user
            appCourse.SignUps.Add(appSignUp);
            appUser.SignUps.Add(appSignUp);
            db.SaveChanges();
            return View(appCourse);
        }
    }
}