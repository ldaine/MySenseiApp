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
        public ActionResult Index(FrontPageModel something)
        {
            /* List<SelectListItem> list = new List<SelectListItem>
             {
             new SelectListItem {  Text = "text1", Value = "11"},
             new SelectListItem { Text = "text2", Value = "12"},
             new SelectListItem {Text = "text3", Value = "13"},
             new SelectListItem {Text = "text4", Value = "14"},
             new SelectListItem { Text = "text5", Value = "15"}
             };*/


            //Cities citiess = new Cities();
            //var allCities =  new IEnumerable<CreateModel();
            //ViewBag.Locations = new SelectList(allCities.City);

            return View(something);
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

        //Search for lessons
        [AllowAnonymous]
        public ActionResult Lessons(string searchString)
        {
            var courses = from c in db.Courses select c;
            if (!string.IsNullOrEmpty(searchString))
            {

                courses = courses.Where(c => c.Headline.ToUpper().Contains(searchString.ToUpper()));
            }

            return View(courses);
        }

        //Location dropdown list
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Location(FrontPageModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Loc = from c in db.Courses where c.Location == model.City.ToString() select c;
            ViewBag.Local = model.City.ToString();
            if (ViewBag.Loc == null)
            {
                return HttpNotFound();
            }
            return View(ViewBag.Loc);

        }

    }

}