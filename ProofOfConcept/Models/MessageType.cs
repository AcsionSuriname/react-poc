using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models
{
    [Table("MessageTypes", Schema = "user")]
    public class MessageType
    {
        public MessageType()
        {
            this.Messages = new HashSet<Message>();
        }

        [Key]
        public short Id { get; set; }
        public string Description_EN { get; set; }
        public bool ToSendOutFlag { get; set; }
        public string Description_NL { get; set; }
        public string Description_ES { get; set; }
        public string Code { get; set; }
        public Nullable<bool> ShowInComposeMailCategory { get; set; }
        public string Color { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }

    }
}
