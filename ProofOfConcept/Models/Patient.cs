using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models
{
    [Table("Patients", Schema = "patient")]
    public class Patient
    {
        public Patient()
        {
            this.Messages = new HashSet<Message>();
        }

        [Key]
        public System.Guid PatientGID { get; set; }
        public System.Guid ContactPersonGID { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string Comments { get; set; }

        //[System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("ContactPersonGID")]
        public virtual Person Person { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }

    }
}
