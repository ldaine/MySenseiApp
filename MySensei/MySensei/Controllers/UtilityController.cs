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

    public class UtilityController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Admin/AdminUtility
        public ActionResult _AllCoursesSearchBox(string searchString)
        {
            var userID = User.Identity.GetUserId();
            var courses = from c in db.Courses select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()));
                //var course = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()) || c => c.Description.ToUpper().Contains(searchString.ToUpper()));
            }


            //  List<AppCourse> something = db.Database.SqlQuery<AppCourse>("select headline from dbo.AppCourses").ToList();

            return PartialView(courses);
        }

        // GET: Admin/AdminUtility
        public ActionResult _CoursesFromCurrentUserSignedUp()
        {
            var userID = User.Identity.GetUserId();
            var courses = from c in db.Courses select c;

            var myCourses = db.Courses.Where(c => c.SignUps.Select(x => x.AppUserID).Contains(userID));

            return PartialView(myCourses);
        }

        public ActionResult _CoursesTeachedByCurrentUser(string searchString)
        {
            var userID = User.Identity.GetUserId();
            var courses = from c in db.Courses select c;

            var myCourses = db.Courses.Where(c => c.AppUserID == userID);

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()));
                //var course = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()) || c => c.Description.ToUpper().Contains(searchString.ToUpper()));
            }


            //  List<AppCourse> something = db.Database.SqlQuery<AppCourse>("select headline from dbo.AppCourses").ToList();
            return PartialView(myCourses);
        }

    }
}