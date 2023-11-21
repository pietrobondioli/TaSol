namespace Web.Controllers.User.DTOs;

public class AuthenticateUserDto
{
    public string UserName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}