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
    [Authorize]
    public class AccountController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Access Denied" });
            }

            //list out the courses
            ViewBag.Appcourses = from c in db.Courses select c;

            ViewBag.returnUrl = returnUrl;
            return View();
        }

        //Categories 
        [AllowAnonymous]
        public ActionResult FrontCategories(int? ids)
        {
            if (ids == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Appp = from c in db.Courses where c.AppCategoryID == ids select c;
            if (ViewBag.Appp == null)
            {
                return HttpNotFound();
            }
            return View(ViewBag.Appp);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(details.UserName, details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);

                    //return Redirect(returnUrl);
                    //role based start page
                    //role Employer go to Employer page
                    if (UserManager.IsInRole(user.Id, "Teacher"))
                    {
                        return RedirectToAction("Index", "Teachers/CourseTeacher");
                    }
                    //role Admin go to Admin page
                    else if (UserManager.IsInRole(user.Id, "Administrators"))
                    {
                        return RedirectToAction("Index", "Admin/Admin");
                    }
                    else
                    {
                        //no role
                        return RedirectToAction("Index", "Home");
                    }

                }
            }

            ViewBag.returnUrl = returnUrl;
                        return View(details);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string role)
        {
            if(role == "teacher")
            {
                return View("RegisterTeacher");

            }
            else if (role == "student")
            {
                return View("RegisterStudent");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
                
        }

        //
        // POST: /Account/RegisterTeacher
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterTeacher(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email 
                    
                    
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, "Teacher");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        // POST: /Account/RegisterStudent
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterStudent(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email, 
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, "Student");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }


        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}