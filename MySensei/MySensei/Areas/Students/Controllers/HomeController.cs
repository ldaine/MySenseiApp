using MySensei.Infrastructure;
using MySensei.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace MySensei.Areas.Students.Controllers
{
    [Authorize(Roles = "Student")]
    public class HomeController : Controller
    {
        // GET: Home
        private AppIdentityDbContext db = new AppIdentityDbContext();
        [Authorize]
        public ViewResult Index(string searchString)
        {
            var userID = User.Identity.GetUserId();
            var courses = from c in db.Courses select c;

            var myCourses = db.Courses.Where(c => c.SignUps.Select(x => x.AppUserID).Contains(userID));
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()));
                //var course = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()) || c => c.Description.ToUpper().Contains(searchString.ToUpper()));
            }


            //  List<AppCourse> something = db.Database.SqlQuery<AppCourse>("select headline from dbo.AppCourses").ToList();

            return View(myCourses);
        }

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
            if(id != null) { appSignUp.AppCourseID = id.GetValueOrDefault(); }
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

        public ActionResult Categories(int? ids)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //AppCategory appCategories = db.AppCategorys.Find(ids);
            //ViewBag.Appp  = db.Courses.Where(b => b.AppCategoryID == ids);
            ViewBag.Appp = from c in db.Courses where c.AppCategoryID == ids select c;
            //appCourse.ToList();
            //AppCourse appCourse = db.Courses.Find(ids);
            //ViewBag.Apppp = appCourse;
            if (ViewBag.Appp == null)
            {
                return HttpNotFound();
            }
            return View(ViewBag.Appp);

        }

        /*   public ActionResult Categories(int? ids)
           {
               if (ids == null)
               {
                   return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
               }
               AppCategory appCategory = db.AppCategorys.Find(db.Courses.Find(ids));
               if (appCategory == null)
               {
                   return HttpNotFound();
               }
               return View(appCategory);
           } */


        /*      [Authorize(Roles = "Teacher")]
              public ActionResult OtherAction()
              {
                  return View("Index", GetData("OtherAction"));
              }



              private Dictionary<string, object> GetData(string actionName)
              {
                  Dictionary<string, object> dict = new Dictionary<string, object>();
                  dict.Add("Action", actionName);
                  dict.Add("User", HttpContext.User.Identity.Name);
                  dict.Add("Authenticated", HttpContext.User.Identity.IsAuthenticated);
                  dict.Add("Auth Type", HttpContext.User.Identity.AuthenticationType);
                  dict.Add("In Users Role", HttpContext.User.IsInRole("Users"));
                  return dict;
              } */
    }
}
