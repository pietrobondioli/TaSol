using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserEmailResetToken : BaseUniqueConsumableToken
{
    [Required]
    public long UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    public static UserEmailResetToken Generate(User user, int expirationInMinutes = 30)
    {
        var token = new UserEmailResetToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes)
        };

        return token;
    }
}
