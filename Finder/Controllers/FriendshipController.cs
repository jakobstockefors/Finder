using Data;
using Data.Models.EF;
using Data.Repositories;
using Finder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finder.Controllers
{
    public class FriendshipController : Controller
    {
        private FinderContext context = new FinderContext();
        // GET: Friendship
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Friendship(string username, string category)
        {
            var rep = new FriendshipRepository(context);
            var userRep = new UserRepository(context);
            var user = userRep.GetUserByUsername(username);
            var pending = rep.GetAllPendingRecievedRequests(user);
            var accepted = rep.GetAllAcceptedFriendships(user);
            var model = new FriendshipsModel() { Friendships = accepted, PendingRecieved = pending, selectedCategory = category };
            return View(model);
        }

        public ActionResult SendFriendRequest(string senderUser, string recieverUser)
        {
            var rep = new FriendshipRepository(context);
            rep.SendFriendRequest(senderUser, recieverUser);
            return RedirectToAction("Profile", "SignedIn", new { username = recieverUser });
        }

        public ActionResult AcceptFriendRequest(string senderUsername, string recieverUsername)
        {
            var rep = new UserRepository(context);
            var friendRep = new FriendshipRepository(context);
            friendRep.AcceptFriendRequest(senderUsername, recieverUsername);
            return RedirectToAction("Friendship", new { username = recieverUsername, category = "Show all friends"});
        }

        public ActionResult RemoveFriendship(string senderUsername, string recieverUsername)
        {
            var friendRep = new FriendshipRepository(context);
            friendRep.RemoveFriendship(senderUsername, recieverUsername);
            return RedirectToAction("Friendship", new { username = User.Identity.Name, category = "Show all friends" });
        }

        
        public ActionResult ChangeCategory(string category, string username, string categoryFilter)
        {
            var rep = new FriendshipRepository(context);
            var friendship = rep.GetFriendship(username, User.Identity.Name);
            rep.UpdateCategory(category, User.Identity.Name, friendship);
            return RedirectToAction("Friendship", new { username = User.Identity.Name, category = categoryFilter });
        }

        public ActionResult FilterByCategory(string category)
        {
            return RedirectToAction("Friendship", new { username = User.Identity.Name, category = category });
        }
    }
}