using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models
{
    [Table("Persons", Schema = "person")]
    public class Person
    {
        public Person()
        {
            this.Patients = new HashSet<Patient>();
        }

        [Key]
        public System.Guid PersonGID { get; set; }
        public string Prefix { get; set; }
        public string IdentificationNumber { get; set; }
        public string Surname { get; set; }
        public string MarriedName { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public Nullable<bool> Dead { get; set; }
        public Nullable<System.DateTime> DeathDate { get; set; }
        public string ProfilePictureFileName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string NHID { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Patient> Patients { get; set; }

    }
}
