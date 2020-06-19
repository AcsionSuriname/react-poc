using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models
{
    [Table("Messages", Schema = "user")]
    public class Message
    {
        public Message()
        {
            this.Messages1 = new HashSet<Message>();
            this.MessageRecipients = new HashSet<MessageRecipient>();
        }

        [Key]
        public System.Guid MessageGID { get; set; }
        public short MessageTypeId { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string SubjectText { get; set; }
        public string BodyText { get; set; }
        public string SubjectId { get; set; }
        public Nullable<System.Guid> SubjectGID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string Messages_TemporaryAttachmentPath { get; set; }
        public short SentStatus { get; set; }
        public short Priority { get; set; }
        public Nullable<System.Guid> ParentMessageGID { get; set; }
        public Nullable<System.Guid> PatientGID { get; set; }
        public string PlainBodyText { get; set; }
        public string BodyText_EN { get; set; }
        public string BodyText_NL { get; set; }
        public string BodyText_ES { get; set; }
        public string SubjectText_EN { get; set; }
        public string SubjectText_NL { get; set; }
        public string SubjectText_ES { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("PatientGID")]
        public virtual Patient Patient { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("MessageTypeId")]
        public virtual MessageType MessageType { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Message> Messages1 { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("ParentMessageGID")]
        public virtual Message Message1 { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<MessageRecipient> MessageRecipients { get; set; }
    }
}
