using System.Security.Claims;

namespace OWASP10_2021.Models
{
    public class User
    {
        public string Id { get; internal set; }
        public string Username { get; internal set; }
        public string PasswordHash { get; internal set; }
        public string Role { get; internal set; }

        public static User GetUserFromClaim(ClaimsPrincipal claimPrincipal)
        {
            return new User
            {
                Id = claimPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value,
                Username = claimPrincipal.FindFirst(ClaimTypes.Name).Value,
                Role = claimPrincipal.FindFirst(ClaimTypes.Role).Value
            };
        }
    }
}
