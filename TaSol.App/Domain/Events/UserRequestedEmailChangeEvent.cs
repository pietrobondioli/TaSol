namespace Domain.Events;

public class UserRequestedEmailChangeEvent : BaseEvent
{
    public UserRequestedEmailChangeEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}