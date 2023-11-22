using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserEmailVerificationToken : BaseUniqueConsumableToken
{
    [Required]
    public long UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;

    public static UserEmailVerificationToken Generate(User user, int expirationInMinutes = 3600)
    {
        var token = new UserEmailVerificationToken
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes)
        };

        return token;
    }
}
