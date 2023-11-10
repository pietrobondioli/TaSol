namespace Domain.Events;

public class UserRequestedNewVerificationTokenEvent : BaseEvent
{
    public UserRequestedNewVerificationTokenEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}
