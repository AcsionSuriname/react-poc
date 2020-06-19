using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models
{
    [Table("MessageRecipients", Schema = "user")]
    public class MessageRecipient
    {
        [Key]
        public System.Guid MessageRecipientGID { get; set; }
        public System.Guid MessageGID { get; set; }
        public string UserGID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FromUserId { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public Nullable<short> SentTries { get; set; }
        public Nullable<System.DateTime> DateSent { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public bool MessageRead { get; set; }
        public Nullable<System.DateTime> DateRead { get; set; }
        public short SentStatus { get; set; }
        public bool IsArchivedFlag { get; set; }
        public bool Unfollow { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("MessageGID")]
        public virtual Message Message { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("UserGID")]
        public virtual User User { get; set; }
    }
}
