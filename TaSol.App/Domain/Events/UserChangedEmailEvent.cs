namespace Domain.Events;

public class UserChangedEmailEvent : BaseEvent
{
    public UserChangedEmailEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}
