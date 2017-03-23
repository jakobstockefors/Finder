using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.EF
{
    public class Visit
    {
        public virtual int VisitId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual int VisitorId { get; set; }
        public virtual int VisitedId { get; set; }
        
        [ForeignKey("VisitorId")]
        [InverseProperty("Visitors")]
        public virtual User Visitor { get; set; }

        [ForeignKey("VisitedId")]
        [InverseProperty("Visited")]
        public virtual User Visited { get; set; }
    }
}
