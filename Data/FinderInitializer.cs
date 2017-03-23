//using Data.Models.EF;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Data
//{
//    public class FinderInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<FinderContext>
//    {
//        protected override void Seed(FinderContext context)
//        {
//            var users = new List<User>
//            {
//                new User {FirstName = "Kajsa", LastName = "Bergqvist", UserName = "Kajsa92", Age = 24, Description = "Jag gillar att hoppa", Gender = "Female", Searchable = true, InterestMen = true, InterestWomen = false, Picture = "default.png" },
//                new User {FirstName = "Sven", LastName = "Svensson", UserName = "Svennis48", Age = 48, Description = "Jag gillar att äta mat", Gender = "Male", Searchable = true, InterestMen = false, InterestWomen = true, Picture = "default.png" },
//                new User {FirstName = "Jonas", LastName = "Gardell", UserName = "GardellBoyCool", Age = 45, Description = "Jag gillar Mark", Gender = "Male", Searchable = true, InterestMen = true, InterestWomen = false, Picture = "default.png" }
//            };
//            users.ForEach(s => context.Users.Add(s));

//            var interests = new List<Interest>
//            {
//                new Interest {Name = "Sport"},
//                new Interest {Name = "Politik"},
//                new Interest {Name = "Mat"},
//                new Interest {Name = "Resa"},
//                new Interest {Name = "Musik"}
//            };
//            interests.ForEach(s => context.Interests.Add(s));

//            var friendships = new List<Friendship>
//            {
//                new Friendship { Accepted = true, Recipient = 1, Sender = 2}
//            };
//            friendships.ForEach(s => context.Friendships.Add(s));
//            context.SaveChanges();
//            base.Seed(context);
//        }
//    }
//}
