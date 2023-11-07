using System.Security.Claims;
using Domain.Entities;

namespace Application.Users;

public static class UserClaims
{
    public static List<Claim> GetClaims(this User user)
    {
        var claims = new List<Claim>
        {
            new Claim(Types.Id, user.Id.ToString()),
            new Claim(Types.UserName, user.UserName),
            new Claim(Types.Role, user.Role.ToString()),
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