namespace Domain.Entities;

public class UserMetadata : BaseEntity
{
    public int UserId { get; set; }
    
    public User User { get; set; } = null!;
}