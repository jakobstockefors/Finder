using Data.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PostRepository : IDisposable
    {
        private FinderContext context;

        public PostRepository(FinderContext context)
        {
            this.context = context;
        }

        public void Post(User senderUser, User recieverUser, string message)
        {
            var post = new Post()
            {
                Author = senderUser,
                Reciever = recieverUser,
                Content = message,
                Date = DateTime.Now
            };
            context.Posts.Add(post);
            context.SaveChanges();
        }

        public List<Post> GetPostOnUserWall(User user)
        {
            List<Post> posts = new List<Post>();
            posts = context.Users.FirstOrDefault(x => x.UserName == user.UserName).RecievedPosts.ToList();
            return posts;
        }

        public Post GetPost(int postId)
        {
            var post = new Post();
            post = context.Posts.FirstOrDefault(x => x.PostId == postId);
            return post;
        }
        public bool DeletePost(int postId)
        {
            try
            {
                context.Posts.Remove(GetPost(postId));
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
