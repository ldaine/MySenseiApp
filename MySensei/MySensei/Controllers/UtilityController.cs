using Microsoft.AspNet.Identity;
using MySensei.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}