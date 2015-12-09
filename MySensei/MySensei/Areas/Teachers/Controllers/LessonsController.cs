using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySensei.Areas.Teachers.Controllers
{
    public class LessonsController : Controller
    {
        // GET: Teachers/Lesson
        public ActionResult Index()
        {
            return View();
        }
    }
}