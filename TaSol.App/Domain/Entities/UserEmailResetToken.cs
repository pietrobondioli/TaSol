using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserEmailResetToken : BaseUniqueConsumableToken
{
    public UserEmailResetToken(User user, int expirationInMinutes = 30)
    {
        UserId = user.Id;
        Token = Guid.NewGuid().ToString();
        ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes);
    }

    [Required] public long UserId { get; set; }

    [ForeignKey(nameof(UserId))] public virtual User User { get; set; } = null!;
}
