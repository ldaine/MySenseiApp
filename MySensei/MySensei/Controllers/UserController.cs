using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Infrastructure;
using MySensei.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MySensei.Controllers
{
    public class UserController : Controller
    {
        // GET: Students/User
        public ActionResult Index()
        {

            var UserId = User.Identity.GetUserId();
            AppUser user = UserManager.FindById(UserId);
            return View(user);
        }

        // Edit
        public async Task<ActionResult> Edit()
        {
            var UserId = User.Identity.GetUserId();
            AppUser user = await UserManager.FindByIdAsync(UserId);
            if (user != null)
            {
                UserEditModel editUser = new UserEditModel
                {
                    ID = UserId,
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
            return View(model);
        }


        //Helpers
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