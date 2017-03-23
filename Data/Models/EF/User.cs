using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.EF
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public bool InterestMen { get; set; }
        public bool InterestWomen { get; set; }
        public bool Searchable { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Friendship> FriendshipsRecieved { get; set; }
        public virtual ICollection<Friendship> FriendshipsSent { get; set; }
        public virtual ICollection<Post> RecievedPosts { get; set; }
        public virtual ICollection<Post> SentPosts { get; set; }
        public virtual ICollection<Visit> Visitors  { get; set; }
        public virtual ICollection<Visit> Visited { get; set; }
        public User() : base()
        {
            FriendshipsRecieved = new List<Friendship>();
            FriendshipsSent = new List<Friendship>();
            RecievedPosts = new List<Post>();
            SentPosts = new List<Post>();
            Visitors = new List<Visit>();
            Visited = new List<Visit>();
        }
    }
}
