using Microsoft.AspNet.Identity;
using MySensei.Infrastructure;
using MySensei.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MySensei.Areas.Teachers.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class HomeController : Controller
    {
        // GET: Home
        private AppIdentityDbContext db = new AppIdentityDbContext();
        [Authorize]
        public ViewResult Index(string searchString)
        {
            return View();
        }
    }

}
