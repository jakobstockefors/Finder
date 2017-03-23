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
    public class SearchController : Controller
    {
        private FinderContext context = new FinderContext();
        // GET: Search
        [HttpPost]
        public ActionResult Search(string search)
        {
            var rep = new UserRepository(context);
            var matches = rep.SearchUsers(search);
            var model = new SearchModel() { Matches = matches };
            return View(model);
        }
    }
}