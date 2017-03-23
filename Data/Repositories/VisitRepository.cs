using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.EF;

namespace Data.Repositories
{
    public class VisitRepository
    {
        private FinderContext context;

        public VisitRepository(FinderContext context)
        {
            this.context = context;
        }

        public bool AddVisit(string visitor, string visited)
        {
            try
            {
                var userRep = new UserRepository(context);
                var visitorUser = userRep.GetUserByUsername(visitor);
                var visitedUser = userRep.GetUserByUsername(visited);

                context.Visits.Add(new Visit
                {
                    Visitor = visitorUser,
                    Visited = visitedUser,
                    Date = DateTime.Now
                });
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public List<Visit> GetVisits(User user)
        {
            var visitList = context.Users.FirstOrDefault(x => x.UserName == user.UserName).Visited.ToList();
            if(visitList != null)
            {
                var listWithFive = (from f in visitList select f).Take(5).Reverse().ToList();
                return listWithFive;
            }
            return null;
        }

        public List<User> GetVisitsUserObjects(User user)
        {
            var visitList = GetVisits(user);
            List<User> visitUserList = new List<User>();
            foreach(var visitorEntry in visitList)
            {
                visitUserList.Add(visitorEntry.Visitor);
            }
            if(visitUserList != null)
            {
                return visitUserList;
            }
            return null;
        }
    }
    }
