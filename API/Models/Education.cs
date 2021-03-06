using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_M_Education")]
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }

        //[ForeignKey("University")]
        public int UniversityId { get; set; }
        public virtual ICollection<Profiling> Profiling { get; set; }
        public virtual University University { get; set; }
    }
}
