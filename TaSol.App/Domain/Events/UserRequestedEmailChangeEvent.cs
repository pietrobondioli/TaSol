namespace Domain.Events;

public class UserRequestedEmailChangeEvent : BaseEvent
{
    public UserRequestedEmailChangeEvent(User user, UserEmailResetToken userEmailResetToken)
    {
        User = user;
        UserEmailResetToken = userEmailResetToken;
    }

    public User User { get; }

    public UserEmailResetToken UserEmailResetToken { get; }
}
