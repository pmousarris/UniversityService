using ModelUtil;
using System.Security.Claims;
using UnicService.ViewModels.Login;

namespace UnicService
{
    public class Util
    {
        /// <summary>
        /// Extracts and returns the user claim information from the given ClaimsPrincipal.
        /// </summary>
        /// <param name="claimsPrincipal">The ClaimsPrincipal from which to extract the user claim information.</param>
        /// <returns>A UserClaim object containing the user's ID and role if the ClaimsPrincipal contains these claims; otherwise, null.</returns>
        public static UserClaim GetUserClaim(ClaimsPrincipal claimsPrincipal)
        {
            UserClaim userClaim = null;

            if (claimsPrincipal != null && claimsPrincipal.HasClaim(c => c.Type == nameof(UserClaim.UserId)))
            {
                Guid userId = Guid.Parse(claimsPrincipal.FindFirstValue(nameof(UserClaim.UserId)));
                Enum.TryParse(claimsPrincipal.FindFirstValue(nameof(UserClaim.UserRole)), out UserRole userRole);
                userClaim = new UserClaim(userId: userId, userRole: userRole);
            }

            return userClaim;
        }
    }
}
