using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers.UserChangedEmail;

public class SendEmailToNewEmailEventHandler : INotificationHandler<UserChangedEmailEvent>
{
    private readonly ILogger<UserVerifiedAccountEventHandler> _logger;
    private readonly IMailService _mailService;

    public SendEmailToNewEmailEventHandler(ILogger<UserVerifiedAccountEventHandler> logger, IMailService mailService)
    {
        _logger = logger;
        _mailService = mailService;
    }

    public Task Handle(UserChangedEmailEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.User.Email, "Email Changed", "Your email has been changed.");

        return Task.CompletedTask;
    }
}
