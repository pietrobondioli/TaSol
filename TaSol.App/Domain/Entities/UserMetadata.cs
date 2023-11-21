using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserMetadata : BaseEntity
{
    public DateTimeOffset? LastLoggedIn { get; set; }

    public DateTimeOffset? LastActive { get; set; }

    public DateTimeOffset? LastPasswordChanged { get; set; }

    public DateTimeOffset? LastLockout { get; set; }

    public DateTimeOffset? LastChangedEmail { get; set; }

    public long UserId { get; set; }

    [ForeignKey(nameof(UserId))] public virtual User User { get; set; } = null!;
}
