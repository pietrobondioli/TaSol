using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string UserName { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Role { get; set; }

    public string? RefreshToken { get; set; }

    public DateTimeOffset? RefreshTokenExpiresAt { get; set; }

    public bool IsVerified { get; set; }

    public long MetadataId { get; set; }

    [ForeignKey(nameof(MetadataId))] public UserMetadata Metadata { get; set; } = null!;

    public ICollection<UserPasswordResetToken> PasswordResetTokens { get; set; }

    public ICollection<UserEmailResetToken> EmailResetTokens { get; set; }

    public ICollection<UserEmailVerificationToken> EmailVerificationTokens { get; set; }

    public bool IsInRole(string role)
    {
        return Role == role;
    }
}