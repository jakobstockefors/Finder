using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.EF
{
    public class Post
    {
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public int RecieverId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("SentPosts")]
        public virtual User Author { get; set; }
        [ForeignKey("RecieverId")]
        [InverseProperty("RecievedPosts")]
        public virtual User Reciever { get; set; }
    }
}
