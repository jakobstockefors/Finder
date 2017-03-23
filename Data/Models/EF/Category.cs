using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.EF
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Friendship> RequesterCategory { get; set; }
        public virtual ICollection<Friendship> RecieverCategory { get; set; }

        public Category()
        {
            RequesterCategory = new List<Friendship>();
            RecieverCategory = new List<Friendship>();
        }
    }
}
