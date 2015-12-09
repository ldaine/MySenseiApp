using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySensei.Areas.Teachers.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class CourseController : Controller
    {
        // GET: Teachers/Course
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}