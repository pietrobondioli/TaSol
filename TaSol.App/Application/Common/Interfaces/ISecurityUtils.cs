namespace Application.Common.Interfaces;

public interface ISecurityUtils
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);

    string GenerateRandomPassword(int length = 8);

    string GenerateRandomApiKey(int length = 32);
}
