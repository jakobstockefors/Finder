using Data;
using Data.Models.EF;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Finder.Controllers.Api
{
    public class PostController : ApiController
    {
        private FinderContext context = new FinderContext();
        public void PostMessage(string message, string reciever)
        {
            var rep = new PostRepository(context);
            var userrep = new UserRepository(context);
            User senderUser = userrep.GetUserByUsername(User.Identity.Name);
            User recieverUser = userrep.GetUserByUsername(reciever);

            rep.Post(senderUser, recieverUser, message);
            context.SaveChanges();
        }
    }
}
