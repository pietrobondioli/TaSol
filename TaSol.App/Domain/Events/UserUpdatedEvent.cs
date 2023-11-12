namespace Domain.Events;

public class UserUpdatedEvent : BaseEvent
{
    public UserUpdatedEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}