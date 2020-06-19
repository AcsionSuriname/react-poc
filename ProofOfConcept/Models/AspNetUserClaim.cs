using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Models
{
    [Table("AspNetUserClaims", Schema = "dbo")]
    public class AspNetUserClaim
    {
        public enum ROLE { ADMIN, DEFAULT }

        [Key]
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
