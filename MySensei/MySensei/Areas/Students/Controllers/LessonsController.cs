using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySensei.Areas.Students.Controllers
{
    public class LessonsController : Controller
    {
        // GET: Students/Lessons
        public ActionResult Index()
        {
            return View();
        }
    }
}