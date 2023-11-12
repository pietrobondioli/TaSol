using System.Security.Claims;
using Domain.Entities;

namespace Application.Users;

public static class UserClaims
{
    public static List<Claim> GetClaims(this User user)
    {
        var claims = new List<Claim>
        {
            new(Types.Id, user.Id.ToString()),
            new(Types.UserName, user.UserName),
            new(Types.Role, user.Role)
        };

        return claims;
    }

    private static class Types
    {
        public const string Id = ClaimTypes.NameIdentifier;

        public const string UserName = ClaimTypes.Name;

        public const string Role = ClaimTypes.Role;
    }
}