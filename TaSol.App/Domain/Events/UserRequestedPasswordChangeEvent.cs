namespace Domain.Events;

public class UserRequestedPasswordChangeEvent : BaseEvent
{
    public UserRequestedPasswordChangeEvent(User user, UserPasswordResetToken userPasswordResetToken)
    {
        User = user;
        UserPasswordResetToken = userPasswordResetToken;
    }

    public User User { get; }

    public UserPasswordResetToken UserPasswordResetToken { get; }
}
