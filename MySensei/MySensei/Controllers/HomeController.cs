﻿using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using System.Security.Principal;
using System.Threading.Tasks;
using MySensei.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Models;

namespace MySensei.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private AppIdentityDbContext db = new AppIdentityDbContext();
        [Authorize]
        public ActionResult Index()
        {
            List<AppCourse> ObjCourse = new List<AppCourse>();
            AppCourse Obj = new AppCourse();
            Obj.Headline = "Something";
            ObjCourse.Add(Obj);
            return View(db.Courses);
        }


        
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