using Data.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : IDisposable
    {
        private FinderContext context;

        public UserRepository(FinderContext context)
        {
            this.context = context;
        }

        public List<User> GetAllUsers()
        {
            List<User> testData = new List<User>();
            testData = context.Users.ToList();
            if (testData.Count != 0)
            {
                return testData;
            }
            else
            {
                User myUser = new User()
                {
                    Age = 21,
                    Description = "Hej",
                    FirstName = "Karl",
                    Gender = "Male",
                    LastName = "Woho"
                };
                testData.Add(myUser);
                return testData;
            }
        }

        public bool IsMatch(User user1, User user2)
        {
            bool match = false;
            bool user1Gender = false;
            bool user2Gender = false;
            if (user1.Gender == "Male")
            {
                user1Gender = true;
            }
            if(user2.Gender == "Male")
            {
                user2Gender = true;
            }
            
            if((user1.InterestMen == true && user2Gender == true) || (user1.InterestWomen && user2Gender == false))
            {
                if((user2.InterestMen == true && user1Gender == true) || (user2.InterestWomen && user1Gender == false))
                {
                    match = true;
                }
            }
            return match;
        }

        public List<User> GetAllMatches(User user)
        {
            var allUsers = GetAllUsers();
            List<User> matchingUsers = new List<User>();
            foreach(var u in allUsers)
            {
                if(IsMatch(user, u))
                {
                    matchingUsers.Add(u);
                }
            }
            return matchingUsers;
        }

        public void ChangeAccountStatus(string username, bool status)
        {
            context.Users.FirstOrDefault(x => x.UserName == username).Active = status;
            context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                return context.Users.FirstOrDefault(x => x.UserName == username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CheckIfUsernameIsUnique(string username)
        {
            var user = GetUserByUsername(username);
            if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();

        }

        public User LoginUser(string username, string password)
        {
            try
            {
                return context.Users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                                                         x.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public void UpdateUserProfile(User user)
        {
            var userName = user.UserName;

            context.Users.FirstOrDefault(x => x.UserName == userName).Description = user.Description;
            context.Users.FirstOrDefault(x => x.UserName == userName).Searchable = user.Searchable;
            context.SaveChanges();
        }

        public List<User> SearchUsers(string searchString)
        {
            var searchX = searchString;
            if(searchString == null)
            {
                searchX = "";
            }
            var search = searchX.ToLower();
            var matches = new List<User>();

            foreach (User user in context.Users)
            {
                if (search.Contains(user.UserName.ToLower()) ||
                   search.Contains(user.FirstName.ToLower()) ||
                   search.Contains(user.LastName.ToLower()) ||
                   user.FirstName.ToLower().StartsWith(search) ||
                   user.LastName.ToLower().StartsWith(search) ||
                   user.UserName.ToLower().StartsWith(search))
                {
                    if (user.Searchable && user.Active)
                    {
                        matches.Add(user);
                    }
                }
            }
            return matches;
        }

        public List<User> GetFiveRandomUsers()
        {
            try
            {
                var random = new Random();
                var users = new List<User>();
                users = context.Users.Where(user => user.Searchable && user.Active).ToList();
                users = users.OrderBy(user => random.Next()).Take(5).ToList();
                return users;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public void UpdatePersonalInfo(User user)
        {
            var userName = user.UserName;
            context.Users.FirstOrDefault(x => x.UserName == userName).Age = user.Age;
            context.Users.FirstOrDefault(x => x.UserName == userName).FirstName = user.FirstName;
            context.Users.FirstOrDefault(x => x.UserName == userName).LastName = user.LastName;
            context.Users.FirstOrDefault(x => x.UserName == userName).InterestMen = user.InterestMen;
            context.Users.FirstOrDefault(x => x.UserName == userName).InterestWomen = user.InterestWomen;
            context.Users.FirstOrDefault(x => x.UserName == userName).Gender = user.Gender;
            context.SaveChanges();
        }

        public bool ChangePassword(User user, string oldPassword, string newPassword)
        {
            if(user.Password == oldPassword)
            {
                context.Users.FirstOrDefault(x => x.UserName == user.UserName).Password = newPassword;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateProfilePicture(User user)
        {
            context.Users.FirstOrDefault(x => x.UserName == user.UserName).Picture = user.Picture;
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
