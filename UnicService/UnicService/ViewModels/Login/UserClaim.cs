using ModelUtil;

namespace UnicService.ViewModels.Login
{
    public class UserClaim
    {
        public Guid UserId { get; set; }
        public UserRole UserRole { get; set; }

        public UserClaim(Guid userId, UserRole userRole)
        {
            UserId = userId;
            UserRole = userRole;
        }
    }
}
