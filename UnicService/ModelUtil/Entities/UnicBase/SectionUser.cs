using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Entities.UnicBase
{
    [Table("SectionUsers")]
    public class SectionUser
    {
        [ForeignKey(nameof(Section))]
        public int SectionId { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual Section Section { get; set; }
        public virtual User User { get; set; }
    }
}
