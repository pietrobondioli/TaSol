using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers.UserVerifiedAccount;

public class SendAccountVerifiedEmailEventHandler : INotificationHandler<UserVerifiedAccountEvent>
{
    private readonly ILogger<UserVerifiedAccountEventHandler> _logger;
    private readonly IMailService _mailService;

    public SendAccountVerifiedEmailEventHandler(ILogger<UserVerifiedAccountEventHandler> logger,
        IMailService mailService)
    {
        _logger = logger;
        _mailService = mailService;
    }

    public Task Handle(UserVerifiedAccountEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.User.Email, "Account Verified", "Your account has been verified.");

        return Task.CompletedTask;
    }
}
