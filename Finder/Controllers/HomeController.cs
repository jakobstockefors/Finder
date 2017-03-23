using Data;
using Data.Repositories;
using Finder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finder.Controllers
{
    public class HomeController : Controller
    {
        private FinderContext context = new FinderContext();
        public ActionResult Index()
        {
            var rep = new UserRepository(context);
            var users = rep.GetFiveRandomUsers();
            var model = new HomeModel { randomUsers = users };
            return View(model);
        }

        public ActionResult Register()
        {
            return RedirectToAction("Register", "Register");
        }
    }
}