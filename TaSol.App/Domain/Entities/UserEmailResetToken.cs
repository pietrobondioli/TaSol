using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserEmailResetToken : BaseUniqueConsumableToken
{
    public long UserId { get; set; }

    [ForeignKey(nameof(UserId))] public User User { get; set; } = null!;

    public UserEmailResetToken(User user, int expirationInMinutes = 30)
    {
        UserId = user.Id;
        Token = Guid.NewGuid().ToString();
        ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes);
    }
}
