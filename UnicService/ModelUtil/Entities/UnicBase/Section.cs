using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Entities.UnicBase
{
    [Table("Sections")]
    public class Section
    {
        public int Id { get; set; }
        public string SectionNumber { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        [ForeignKey(nameof(AcademicPeriod))]
        public int AcademicPeriodId { get; set; }

        public virtual Course Course { get; set; }
        public virtual AcademicPeriod AcademicPeriod { get; set; }
        public virtual ICollection<SectionUser> SectionUsers { get; set; }
    }

}
