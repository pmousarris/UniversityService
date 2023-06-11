using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ModelUtil.Entities.UnicBase
{
    [Table("Courses")]
    public class Course
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        
        public virtual ICollection<Section> Sections { get; set; }
    }

}
