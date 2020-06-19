using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models
{

    [Table("Users", Schema = "user")]
    public class User
    {
        public User()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
        }

        [Key]
        public string UserGID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string PreferredLanguage { get; set; }
        public Nullable<bool> FormsAutoSave { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public Nullable<System.Guid> LastLocationGID { get; set; }
        public string NormalizedEmail { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string NormalizedUserName { get; set; }
        public Nullable<System.DateTime> LockoutEnd { get; set; }
        public System.DateTime LastPasswordChangedDate { get; set; }
        public bool PasswordNeverExpires { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
    }
}
