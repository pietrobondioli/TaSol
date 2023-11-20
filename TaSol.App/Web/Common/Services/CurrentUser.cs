using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Web.Common.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long? Id => Convert.ToInt64(_httpContextAccessor.HttpContext?.User?.FindFirstValue(UserClaims.Id));

    public string? Email => _httpContextAccessor.HttpContext?.User?.FindFirstValue(UserClaims.Email);

    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(UserClaims.UserName);

    public string? Role => _httpContextAccessor.HttpContext?.User?.FindFirstValue(UserClaims.Role);

    public static List<Claim> ExtractClaimsFromUser(User user)
    {
        var claims = new List<Claim>
        {
            new(UserClaims.Id, user.Id.ToString()),
            new(UserClaims.Email, user.Email),
            new(UserClaims.UserName, user.UserName),
            new(UserClaims.Role, user.Role)
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