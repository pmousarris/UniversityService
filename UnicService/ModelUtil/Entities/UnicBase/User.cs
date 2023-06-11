using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUtil.Entities.UnicBase
{
    [Table("Users")]
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Phone1 { get; set; }
        public string Email { get; set; }
        public string? SocialInsuranceNumber { get; set; }
        public string PasswordHash { get; set; }

        public virtual ICollection<SectionUser> SectionUsers { get; set; }
    }
}
