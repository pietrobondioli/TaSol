namespace Domain.Events;

public class UserChangedEmailEvent : BaseEvent
{
    public UserChangedEmailEvent(User user, string oldEmail)
    {
        User = user;
        OldEmail = oldEmail;
    }

    public string OldEmail { get; set; }

    public User User { get; }
}
