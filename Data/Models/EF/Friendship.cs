using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.EF
{
    public class Friendship
    {
        public int FriendshipId { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public bool Accepted { get; set; }
        public int RequesterCategoryId { get; set; }
        public int RecieverCategoryId { get; set; }

        [ForeignKey("RequesterCategoryId")]
        [InverseProperty("RequesterCategory")]
        public virtual Category RequesterCategory { get; set; }
        [ForeignKey("RecieverCategoryId")]
        [InverseProperty("RecieverCategory")]
        public virtual Category RecieverCategory { get; set; }

        [ForeignKey("SenderId")]
        [InverseProperty("FriendshipsSent")]
        public virtual User SenderUser { get; set; }
        [ForeignKey("RecipientId")]
        [InverseProperty("FriendshipsRecieved")]
        public virtual User RecieverUser { get; set; }
    }
}
