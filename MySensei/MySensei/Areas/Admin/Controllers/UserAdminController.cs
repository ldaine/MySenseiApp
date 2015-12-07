using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Infrastructure;
using MySensei.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MySensei.Areas.Admin.Controllers
{

    // GET: Admin/Admin
    [Authorize(Roles = "Administrators")]
    public class UserAdminController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();
        /*    public ActionResult Index(string sortOrder)
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
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");

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
                    PrimaryLanguage = model.PrimaryLanguage.ToString(),
                    CreatedAt = DateTime.Now
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {


                    var addedToRole = await UserManager.AddToRoleAsync(user.Id, model.Role);
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            ViewBag.Role = new SelectList(db.Roles, "Name", "Name");

            return View(model);
        }

        // Edit
        [Authorize(Roles = "Administrators, Teacher")]
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
                
                //Possibility to add users to roles in User view
                //editUser.Roles = new List<string>();
                //foreach (IdentityUserRole role in user.Roles)
                //{
                //    editUser.Roles.Add(role.RoleId);
                //}

                if (user.City != null)
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

                //ViewBag.Roles = new MultiSelectList(db.Roles, "Id", "Name");

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

                        ////looping through existing role
                        //foreach (IdentityUserRole role in user.Roles)
                        //{
                        //    var name = db.Roles.Find(role.RoleId).Name;
                        //    if (model.Roles.Contains(name))
                        //    {
                        //        model.Roles.Remove(name);
                        //    }
                        //    else
                        //    {
                        //        result = await UserManager.RemoveFromRoleAsync(user.Id, name);
                        //        if (!result.Succeeded)
                        //        {
                        //            return View("Error", result.Errors);
                        //        }
                        //    }
                        //}

                        //foreach (string role in model.Roles)
                        //{
                        //    var roleName = db.Roles.Find(role).Name;
                        //    var addedToRole = await UserManager.AddToRoleAsync(user.Id, roleName);

                        //    result = await UserManager.AddToRoleAsync(user.Id, roleName);
                        //    if (!result.Succeeded)
                        //    {
                        //        return View("Error", result.Errors);
                        //    }
                        //}

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
            //ViewBag.Roles = new MultiSelectList(db.Roles, "Name", "Name");

            return View(user);
        }
        // Delete
        [Authorize(Roles = "Administrators, Teacher")]
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