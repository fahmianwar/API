using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    [Table("TB_M_Account")]
    public class Account
    {
        [Key]
        //[ForeignKey("Person")]
        public int NIK { get; set; }
        [JsonIgnore]
        public string Password { get; set; }

        public virtual Person Person { get; set; }
        public virtual Profiling Profiling { get; set; }
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
