using Data;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Finder.Controllers.Api
{
    public class VisitorController : ApiController
    {
        private FinderContext context = new FinderContext();

        [HttpPost]
        public IHttpActionResult GetVisitors(List<string> jsonArr)
        {
            try
            {
                var visit = new VisitRepository(context);
                var userRep = new UserRepository(context);
                var actualUser = userRep.GetUserByUsername(jsonArr[0]);
                var visitList = visit.GetVisitsUserObjects(actualUser);
                List<object> resultList = new List<object>();

                foreach (var visitorObj in visitList)
                {
                    resultList.Add(new
                    {
                        nameof = visitorObj.FirstName + " " + visitorObj.LastName
                    });
                }
                if (resultList != null)
                {
                    return Ok(resultList);
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
            catch(Exception)
            {
                return BadRequest("Something went wrong");
            }
            
        }
    }
}
