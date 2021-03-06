﻿using MySensei.Infrastructure;
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
            return View();
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
