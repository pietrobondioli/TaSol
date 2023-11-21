using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

public abstract class BaseUniqueConsumableToken : BaseAuditableEntity
{
    [Required] public string Token { get; set; }

    [Required] public DateTimeOffset ExpiresAt { get; set; }

    public DateTimeOffset? ConsumedAt { get; set; }

    public string ConsumerIpAddress { get; set; } = null!;

    public DateTimeOffset? RevokedAt { get; set; }

    public bool IsExpired => ExpiresAt < DateTimeOffset.UtcNow;

    public bool IsActive => ConsumedAt == null && RevokedAt == null && !IsExpired;

    public void Consume(string consumerIpAddress = null!)
    {
        if (IsActive)
        {
            ConsumedAt = DateTimeOffset.UtcNow;
            ConsumerIpAddress = consumerIpAddress;
        }
    }

    public void Revoke()
    {
        if (IsActive)
        {
            RevokedAt = DateTimeOffset.UtcNow;
        }
    }
}
