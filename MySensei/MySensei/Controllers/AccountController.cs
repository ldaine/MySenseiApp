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
                return View("AuthError");
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
                    //role Admin go to Admin page
                    if (UserManager.IsInRole(user.Id, "Administrators"))
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    //role Teacher go to Teacher page
                    else if (UserManager.IsInRole(user.Id, "Teacher"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Teachers" });
                    }
                    //role Student go to Student page
                    else if (UserManager.IsInRole(user.Id, "Student"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Students" });
                    }
                    else
                    {
                        //no role
                        return RedirectToAction("Login", "Account", new { area = "" });
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
                return RedirectToAction("Login", "Account", new { area = "" });
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
                    UserName = model.Email,
                    Email = model.Email 
                    
                    
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, "Teacher");

                    return RedirectToAction("Index", "Home", new { area = "Teachers" });
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
                    UserName = model.Email,
                    Email = model.Email, 
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var currentUser = UserManager.FindByName(user.UserName);

                    var roleresult = UserManager.AddToRole(currentUser.Id, "Student");

                    return RedirectToAction("Index", "Home", new { area = "Students"});
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
            return RedirectToAction("Index", "FrontPage", new { area = "" });
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