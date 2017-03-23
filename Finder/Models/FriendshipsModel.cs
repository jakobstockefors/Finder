using Data.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finder.Models
{
    public class FriendshipsModel
    {
        public User user { get; set; }
        public List<Friendship> Friendships { get; set; }
        public List<Friendship> PendingRecieved { get; set; }

        public string selectedCategory { get; set; }
    }
}