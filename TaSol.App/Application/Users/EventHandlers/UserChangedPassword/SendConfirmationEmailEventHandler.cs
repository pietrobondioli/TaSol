using Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Application.Users.EventHandlers.UserChangedPassword;

public class SendConfirmationEmailEventHandler : INotificationHandler<UserChangedPasswordEvent>
{
    private readonly IOptions<AppSettings> _appSettings;
    private readonly ILogger<SendConfirmationEmailEventHandler> _logger;
    private readonly IMailService _mailService;

    public SendConfirmationEmailEventHandler(ILogger<SendConfirmationEmailEventHandler> logger, IMailService mailService,
        IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _mailService = mailService;
        _appSettings = appSettings;
    }

    public Task Handle(UserChangedPasswordEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.User.Email, "Password Changed",
            "Your password has been changed. If you did not request this change, please click here to revert it: " +
            _appSettings.Value.LockAccountUrl);

        return Task.CompletedTask;
    }
}
