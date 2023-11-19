using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    [Required]
    public string UserName { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string Role { get; set; } = Roles.User;

    public string? RefreshToken { get; set; }

    public DateTimeOffset? RefreshTokenExpiresAt { get; set; }

    [Required]
    public bool IsVerified { get; set; }

    [Required]
    public long MetadataId { get; set; }

    [ForeignKey(nameof(MetadataId))]
    public virtual UserMetadata Metadata { get; set; } = null!;

    public virtual ICollection<UserPasswordResetToken> PasswordResetTokens { get; set; } = null!;

    public virtual ICollection<UserEmailResetToken> EmailResetTokens { get; set; } = null!;

    public virtual ICollection<UserEmailVerificationToken> EmailVerificationTokens { get; set; } = null!;

    public bool IsInRole(string role)
    {
        return Role == role;
    }
}
