namespace Domain.Events;

public class UserChangedPasswordEvent : BaseEvent
{
    public UserChangedPasswordEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}