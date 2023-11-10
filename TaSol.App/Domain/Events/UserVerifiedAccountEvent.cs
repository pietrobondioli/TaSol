namespace Domain.Events;

public class UserVerifiedAccountEvent : BaseEvent
{
    public UserVerifiedAccountEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}
