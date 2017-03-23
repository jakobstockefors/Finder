using Data.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finder.Models
{
    public class UserModel
    {
        public User user { get; set; }
        public ICollection<Post> Posts { get; set; }

        public bool Matching { get; set; }
    }
}