namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public Roles Role { get; set; }

    public string? RefreshToken { get; set; }

    public DateTimeOffset? RefreshTokenExpiresAt { get; set; }

    public bool IsVerified { get; set; }

    public DateTimeOffset? LastLoggedIn { get; set; }

    public DateTimeOffset? LastActive { get; set; }

    public DateTimeOffset? LastPasswordChanged { get; set; }

    public DateTimeOffset? LastLockout { get; set; }

    public DateTimeOffset? LastChangedEmail { get; set; }

    public ICollection<UserPasswordResetToken> PasswordResetTokens { get; set; }

    public ICollection<UserEmailResetToken> EmailResetTokens { get; set; }

    public ICollection<UserEmailVerificationToken> EmailVerificationTokens { get; set; }
}