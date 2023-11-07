using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserEmailVerificationToken : BaseUniqueConsumableToken
{
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))] public User User { get; set; } = null!;

    public static UserEmailVerificationToken Create(User user, int expirationInMinutes = 3600)
    {
        return new()
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes)
        };
    }
}