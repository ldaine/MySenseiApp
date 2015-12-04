using System.Threading.Tasks;
using System.Web.Mvc;
using MySensei.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Infrastructure;
using System.Web;
using System.Data;
using System.Linq;
using System.Net;
using System.Collections;

namespace MySensei.Controllers
{

    public class FrontPageController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        // GET: FrontPage
        public ActionResult Index()
        {
            return View();
        }

        //Categories 
        [AllowAnonymous]
        public ActionResult Categories(int? ids)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Categ = from c in db.Courses where c.AppCategoryID == ids select c;
            ViewBag.CategName = db.AppCategorys.Find(ids);
            if (ViewBag.Categ == null)
            {
                return HttpNotFound();
            }
            return View(ViewBag.Categ);

        }

        [AllowAnonymous]
        public ActionResult Courses(string searchString)
        {
            var courses = from c in db.Courses select c;
            if (!string.IsNullOrEmpty(searchString))
            {

                courses = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()));
                //var course = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()) || c => c.Description.ToUpper().Contains(searchString.ToUpper()));
            }
            //  List<AppCourse> something = db.Database.SqlQuery<AppCourse>("select headline from dbo.AppCourses").ToList();

            return View(courses);
        }

        public ActionResult Lessons()
        {
            //AppCourse lessons = db.Courses.Find(2);
            return View();
        }
    }

}