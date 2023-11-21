namespace Web.Common.Interfaces;

public interface IJwtService
{
    string GenerateToken(string id, string email, string userName, string role);
}
