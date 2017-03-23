using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Repositories;
using Data.Models.EF;
using Data.Serialize;
using Data;
using Finder.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Security;
using System.IO;
using System.Xml.Serialization;

namespace Finder.Controllers
{
    public class SignedInController : Controller
    {
        private FinderContext context = new FinderContext();
        // GET: SignedIn
        public ActionResult Index()
        {
            return View();
        }

        public new ActionResult Profile(string username)
        {
            var rep = new UserRepository(context);
            var visitRep = new VisitRepository(context);
            var postRep = new PostRepository(context);
            var visitedUser = rep.GetUserByUsername(username);
            var visitingUser = rep.GetUserByUsername(User.Identity.Name);
            var posts = postRep.GetPostOnUserWall(visitedUser);
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Register");
            }
            else
            {
                var isMatch = rep.IsMatch(visitedUser, visitingUser);
                var ProfileModel = new UserModel()
                {
                    user = visitedUser,
                    Matching = isMatch,
                    Posts = posts                   
                };
                if (!visitedUser.UserName.Equals(visitingUser.UserName))
                {
                    var visitList = visitRep.GetVisitsUserObjects(visitedUser);
                    bool exists = visitList.Contains(visitingUser);
                    if (!exists)
                    {
                        visitRep.AddVisit(visitingUser.UserName, visitedUser.UserName);
                    }
                }
                return View(ProfileModel);
            }   
        }


        [HttpGet]
        public ActionResult UpdateProfileInfo()
        {
            UserModel model = new UserModel();
            UserRepository rep = new UserRepository(context);
            model.user = rep.GetUserByUsername(User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateProfileInfo(UserModel userToUpdate)
        {
            UserRepository rep = new UserRepository(context);
            userToUpdate.user.UserName = User.Identity.Name;
            rep.UpdateUserProfile(userToUpdate.user);
            return RedirectToAction("Profile", new { username = User.Identity.Name });
        }


        public ActionResult AccountSettings(string username)
        {
            var rep = new UserRepository(context);
            var userToChange = rep.GetUserByUsername(username);
            var model = new UserModel();
            model.user = userToChange;
            return View(model);
        }

        public ActionResult ChangeAccountStatus(string username, bool status)
        {
            var rep = new UserRepository(context);
            rep.ChangeAccountStatus(username, status);
            var updatedUser = rep.GetUserByUsername(username);
            var model = new UserModel() { user = updatedUser };
            return RedirectToAction("AccountSettings", new { username = username});
        }

        public ActionResult sendFriendRequest(string username_sender, string username_reciever)
        {
            var rep = new FriendshipRepository(context);
            rep.SendFriendRequest(username_sender, username_reciever);
            return RedirectToAction("FriendRequest");
        }
        
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            UserRepository rep = new UserRepository(context);
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            UserRepository rep = new UserRepository(context);
            var userToUpdate = rep.GetUserByUsername(User.Identity.Name);
            if(rep.ChangePassword(userToUpdate, model.oldPassword, model.newPassword))
            {
                TempData["notice"] = "Password successfully changed";
                return View();
            }
            else
            {
                TempData["notice"] = "Old password was incorrect";
                return View();
            }
        }

        [HttpGet]
        public ActionResult UpdatePersonalInfo(RegisterModel userToUpdate)
        {
            UserRepository rep = new UserRepository(context);
            var model = new UserModel() { user = rep.GetUserByUsername(User.Identity.Name) };
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdatePersonalInfo(UserModel userToUpdate)
        {
            UserRepository rep = new UserRepository(context);
            userToUpdate.user.UserName = User.Identity.Name;
            rep.UpdatePersonalInfo(userToUpdate.user);
            TempData["notice"] = "Information successfully changed";
            return RedirectToAction("Profile", "SignedIn", new { username = userToUpdate.user.UserName });
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string username)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = User.Identity.Name + Path.GetFileName(file.FileName); //Username as prefix to ensure unique filename
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(path);
                    var rep = new UserRepository(context);
                    var user = rep.GetUserByUsername(username);
                    user.Picture = fileName;
                    UpdateProfilePicture(user);
                }
                ViewBag.Message = "Upload successful";
                return RedirectToAction("Profile", "SignedIn", new { username = User.Identity.Name });
            }
            catch
            {
                ViewBag.Message = "Upload failed";
                return RedirectToAction("Profile", "SignedIn", new { username = User.Identity.Name });
            }
        }

        public void UpdateProfilePicture(User user)
        {
            var rep = new UserRepository(context);
            rep.UpdateProfilePicture(user);
        }

        public ActionResult ChangeProfilePicture(UserModel userToUpdate)
        {
            var rep = new UserRepository(context);
            userToUpdate.user = rep.GetUserByUsername(User.Identity.Name);
            return View(userToUpdate);
        }

        [Authorize]
        public ActionResult DeletePost(string postId)
        {
            var rep = new PostRepository(context);
            var idAsInt = Int32.Parse(postId);
            var deleted = rep.DeletePost(idAsInt);
            return RedirectToAction("Profile", new { username = User.Identity.Name });
        }

        public void SerializeProfile(string username)
        {
                var userRep = new UserRepository(context);
                var user = userRep.GetUserByUsername(username);
                var model = new SerializeUserModel
                {
                    FirstName = user.FirstName,
                    Password = user.Password,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Age = user.Age,
                    Gender = user.Gender,
                    Description = user.Description,
                    Picture = user.Picture
                };
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename = " + username + "profile.xml");
            Response.ContentType = "text/xml";
            var serializer = new XmlSerializer(typeof(SerializeUserModel));
            serializer.Serialize(Response.OutputStream, model);
        }

        public ActionResult Matches()
        {
            var rep = new UserRepository(context);
            var user = rep.GetUserByUsername(User.Identity.Name);
            List<User> matchesList = rep.GetAllMatches(user);
            var matches = new MatchesModel()
            {
                Matches = matchesList
            };
            return View(matches);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Register");
        }
    }
}