namespace Domain.Events;

public class UserRequestedNewVerificationTokenEvent : BaseEvent
{
    public UserRequestedNewVerificationTokenEvent(User user, UserEmailVerificationToken userEmailVerificationToken)
    {
        User = user;
        UserEmailVerificationToken = userEmailVerificationToken;
    }

    public User User { get; }

    public UserEmailVerificationToken UserEmailVerificationToken { get; }
}
