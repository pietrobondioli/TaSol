using Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Application.Users.EventHandlers.UserChangedEmail;

public class SendEmailToOldEmailEventHandler : INotificationHandler<UserChangedEmailEvent>
{
    private readonly IOptions<AppSettings> _appSettings;
    private readonly ILogger<SendEmailToOldEmailEventHandler> _logger;
    private readonly IMailService _mailService;

    public SendEmailToOldEmailEventHandler(ILogger<SendEmailToOldEmailEventHandler> logger, IMailService mailService,
        IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _mailService = mailService;
        _appSettings = appSettings;
    }

    public Task Handle(UserChangedEmailEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.OldEmail, "Email Changed",
            "Your email has been changed. If you did not request this change, please click here to revert it: " +
            _appSettings.Value.LockAccountUrl);

        return Task.CompletedTask;
    }
}
