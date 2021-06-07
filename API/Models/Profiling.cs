using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_T_Profiling")]
    public class Profiling
    {
        [Key]
        //[ForeignKey("Account")]
        public int NIK { get; set; }
        public int EducationId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Education Education { get; set; }
    }
}
