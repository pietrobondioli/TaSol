using System.Security.Claims;
using Application.Common.Interfaces;

namespace Web.Common.Services;

public class HttpUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long? Id => _httpContextAccessor.HttpContext?.User.FindFirstValue(UserClaims.Id) is { } id
        ? Convert.ToInt64(id)
        : null;

    public string? Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(UserClaims.Email);

    public string? UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue(UserClaims.UserName);

    public string? Role => _httpContextAccessor.HttpContext?.User.FindFirstValue(UserClaims.Role);

    public static List<Claim> GenerateClaims(string id, string email, string userName, string role)
    {
        var claims = new List<Claim>
        {
            new(UserClaims.Id, id),
            new(UserClaims.Email, email),
            new(UserClaims.UserName, userName),
            new(UserClaims.Role, role)
        };

        return claims;
    }

    private static class UserClaims
    {
        public const string Id = ClaimTypes.NameIdentifier;

        public const string Email = ClaimTypes.Email;

        public const string UserName = ClaimTypes.Name;

        public const string Role = ClaimTypes.Role;
    }
}
