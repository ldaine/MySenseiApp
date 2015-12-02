using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Infrastructure;
using MySensei.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace MySensei.Controllers
{

    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        /*  private AppIdentityDbContext db = new AppIdentityDbContext();
          public ActionResult Index(string sortOrder)
          {
              ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
              ViewBag.DateSortParm = sortOrder == "Email" ? "email_desc" : "Email";
              var userss = from s in db.Database.SqlQuery<AppUser>("select * from dbo.AppUser") select s;
              switch (sortOrder)
              {
                  case "name_desc":
                      userss = userss.OrderByDescending(s => s.UserName);
                      break;
                  case "Email":
                      userss = userss.OrderBy(s => s.Email);
                      break;
                  case "email_desc":
                      userss = userss.OrderByDescending(s => s.Email);
                      break;
                  default:
                      userss = userss.OrderBy(s => s.UserName);
                      break;
              }
              return View(UserManager.Users);
          }  */

        public ActionResult Index()
        {
            return View(UserManager.Users);
        }


        public ActionResult Create()
        {
            return View();
        }



        // Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Address = model.Address,
                    Zip = model.Zip,
                    City = model.City.ToString(),
                    Country = "Danmark",
                    Avatar = model.Avatar,
                    Gender = model.Gender.ToString(),
                    Biography = model.Biography,
                    Birthday = model.Birthday,
                    PrimaryLanguage = model.PrimaryLanguage.ToString()
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        // Edit
        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                UserEditModel editUser = new UserEditModel
                {
                    ID = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Address = user.Address,
                    Zip = user.Zip,
                    Country = user.Country,
                    Avatar = user.Avatar,
                    Biography = user.Biography,
                    Birthday = user.Birthday,
                };
                if(user.City != null)
                {
                    editUser.City = (Cities)System.Enum.Parse(typeof(Cities), user.City);
                }
                if (user.Gender != null)
                {
                    editUser.Gender = (Gender)System.Enum.Parse(typeof(Gender), user.Gender);
                }

                if (user.City != null)
                {
                    editUser.PrimaryLanguage = (Language)System.Enum.Parse(typeof(Language), user.PrimaryLanguage);
                }
                return View(editUser);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserEditModel model, string password)
        {
            AppUser user = await UserManager.FindByIdAsync(model.ID);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;

                user.Address = model.Address;
                user.Zip = model.Zip;
                user.City = model.City.ToString();
                user.Country = "Danmark";
                user.Avatar = model.Avatar;
                user.Gender = model.Gender.ToString();
                user.Biography = model.Biography;
                user.Birthday = model.Birthday;
                user.PrimaryLanguage = model.PrimaryLanguage.ToString();

                user.Email = model.Email;

                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }
            return View(user);
        }
        // Delete
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "User Not Found" });
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}