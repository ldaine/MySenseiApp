using MySensei.Infrastructure;
using MySensei.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySensei.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        private AppIdentityDbContext db = new AppIdentityDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CleanTagTable()
        {
            db.Database.ExecuteSqlCommand("sp_CleanTagTable");
            return View();
        }

    }
}