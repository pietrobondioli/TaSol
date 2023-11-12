namespace Domain.Events;

public class UserRegisteredEvent : BaseEvent
{
    public UserRegisteredEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}