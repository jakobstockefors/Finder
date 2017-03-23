using Data.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finder.Models
{
    public class SearchModel
    {
        public List<User> Matches { get; set; }
    }
}