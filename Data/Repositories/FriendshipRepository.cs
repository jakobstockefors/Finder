using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.EF;

namespace Data.Repositories
{
    public class FriendshipRepository : IDisposable
    {
        private FinderContext context;

        public FriendshipRepository(FinderContext context)
        {
            this.context = context;
        }

        public void SendFriendRequest(string sender, string reciever)
        {
            var request = new Friendship();
            var userRep = new UserRepository(context);
            var friendRep = new FriendshipRepository(context);
            request.SenderUser = userRep.GetUserByUsername(sender);
            request.RecieverUser = userRep.GetUserByUsername(reciever);
            request.RecieverCategory = context.Categories.FirstOrDefault(x => x.CategoryId == 1);
            request.RequesterCategory = context.Categories.FirstOrDefault(x => x.CategoryId == 1);
            context.Friendships.Add(request);
            context.SaveChanges();
        }

        //Gets all the accepted friendships for a user
        public List<Friendship> GetAllAcceptedFriendships(User user)
        {
            var rep = new UserRepository(context);
            var recieved = context.Users.FirstOrDefault(x => x.UserName == user.UserName).FriendshipsRecieved.ToList();
            var sent = context.Users.FirstOrDefault(x => x.UserName == user.UserName).FriendshipsSent.ToList();
            var acceptedFriendships = new List<Friendship>();
            foreach (var friendship in recieved)
            {
                if (friendship.Accepted == true && friendship.SenderUser.Active == true)
                {
                    acceptedFriendships.Add(friendship);
                }
            }
            foreach (var friendship in sent)
            {
                if (friendship.Accepted == true && friendship.RecieverUser.Active == true)
                {
                    acceptedFriendships.Add(friendship);
                }
            }
            return acceptedFriendships;
        }


        //Gets all pending recieved friendrequests for a user
        public List<Friendship> GetAllPendingRecievedRequests(User user)
        {
            try
            {
                var rep = new UserRepository(context);
                var recieved = context.Users.FirstOrDefault(x => x.UserName == user.UserName).FriendshipsRecieved.ToList();
                var pendingRequests = new List<Friendship>();
                foreach (var friendship in recieved)
                {
                    if (friendship.Accepted == false && friendship.SenderUser.Active == true)
                    {
                        pendingRequests.Add(friendship);
                    }
                }
                return pendingRequests;
            }
            catch(Exception)
            {
                return null;
            }
            
        }

        public void AcceptFriendRequest(string senderUsername, string recieverUsername)
        {
            var rep = new UserRepository(context);
            var sender = rep.GetUserByUsername(senderUsername);
            var reciever = rep.GetUserByUsername(recieverUsername);
            context.Friendships.FirstOrDefault(x => x.RecipientId == reciever.UserID && x.SenderId == sender.UserID).Accepted = true;
            context.SaveChanges();
        }

        public void RemoveFriendship(string senderUsername, string recieverUsername)
        {
            var friendship = new Friendship();
            friendship = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == recieverUsername && x.SenderUser.UserName == senderUsername);
            if(friendship != null)
            {
                context.Friendships.Remove(friendship);
            }
            else
            {
                friendship = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == senderUsername && x.SenderUser.UserName == recieverUsername);
                context.Friendships.Remove(friendship);
            }
            context.SaveChanges();
        }

        public bool CheckIfFriends(string senderUsername, string recieverUsername)
        {

            var friendship = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == senderUsername && x.SenderUser.UserName == recieverUsername && x.Accepted == true);
            if(friendship == null)
            {
                var friendship1 = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == recieverUsername && x.SenderUser.UserName == senderUsername && x.Accepted == true);
                if(friendship1 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public bool CheckIfFriendrequestPending(string senderUsername, string recieverUsername)
        {

            var friendship = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == senderUsername && x.SenderUser.UserName == recieverUsername && x.Accepted == false);
            if (friendship == null)
            {
                var friendship1 = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == recieverUsername && x.SenderUser.UserName == senderUsername && x.Accepted == false);
                if (friendship1 == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }


        public bool CheckIfUserIsSender(string username, Friendship friendship)
        {
            var isSender = context.Friendships.FirstOrDefault(x => x.SenderUser.UserName == username && x.FriendshipId == friendship
            .FriendshipId);
            if(isSender != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateCategory(string categoryName, string signedInUsername, Friendship friendship)
        {
            try
            {
                var catId = context.Categories.FirstOrDefault(x => x.CategoryName == categoryName).CategoryId;
                if (CheckIfUserIsSender(signedInUsername, friendship))
                {
                    context.Friendships.FirstOrDefault(x => x.SenderUser.UserName == signedInUsername).RecieverCategoryId = catId;
                }
                else
                {
                    context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == signedInUsername).RequesterCategoryId = catId;
                }
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Friendship GetFriendship(string user1, string user2)
        {
            try
            {
                var friendship = new Friendship();
                var rep = new UserRepository(context);
                if(CheckIfFriends(user1, user2))
                {
                    friendship = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == user1 && x.SenderUser.UserName == user2);
                    if (friendship == null)
                    {
                        friendship = context.Friendships.FirstOrDefault(x => x.RecieverUser.UserName == user2 && x.SenderUser.UserName == user1);
                    }
                }
                return friendship;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Category> GetAllCategories()
        {
            return context.Categories.ToList();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
