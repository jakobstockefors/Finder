using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories;
using Data;
using Data.Models.EF;
using Finder.Models;
using System.Web.Security;

namespace Finder.Controllers
{
    public class RegisterController : Controller
    {
        private FinderContext context = new FinderContext();
        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var username = model.Username;
            var password = model.Password;
            var rep = new UserRepository(context);

            var userLogginIn = rep.LoginUser(username, password);

            if (userLogginIn == null) //avbryter och skickar felmeddelande om loginnet är felaktigt.
            {
                model.ErrorMessage = "Wrong username or password"; //
                return View(model);
            }
            if(System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }
            FormsAuthentication.SetAuthCookie(userLogginIn.UserName, false);
            if(userLogginIn.Active == true)
            {
                return RedirectToAction("Profile", "SignedIn", new { username = userLogginIn.UserName });
            }
            else
            {
                return RedirectToAction("AccountSettings", "SignedIn", new { username = userLogginIn.UserName });
            }
            
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newUser = new User();

            newUser.FirstName = model.FirstName;
            newUser.LastName = model.LastName;
            newUser.UserName = model.Username;
            newUser.Password = model.Password;
            newUser.InterestMen = model.IntrestMen;
            newUser.InterestWomen = model.IntrestWomen;
            newUser.Searchable = true;
            newUser.Picture = "profile_default.jpg";
            newUser.Age = model.Age;
            newUser.Description = model.Description;
            newUser.Active = true;

            if (model.Gender.Equals("Male"))
            {
                newUser.Gender = "Male";
            }
            else
            {
                newUser.Gender = "Female";
            }

            var userData = new UserRepository(context);
                if (!userData.CheckIfUsernameIsUnique(newUser.UserName))
                {
                    ModelState.AddModelError("", "Username already exists");
                    return View();
                }
                userData.AddUser(newUser);

            return RedirectToAction("Index", "Home");
        }


    }
}