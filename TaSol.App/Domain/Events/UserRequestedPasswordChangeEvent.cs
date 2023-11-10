namespace Domain.Events;

public class UserRequestedPasswordChangeEvent : BaseEvent
{
    public UserRequestedPasswordChangeEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}
