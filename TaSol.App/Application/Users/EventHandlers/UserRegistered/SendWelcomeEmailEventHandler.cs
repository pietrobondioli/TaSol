using Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Application.Users.EventHandlers.UserRegistered;

public class SendWelcomeEmailEventHandler : INotificationHandler<UserRegisteredEvent>
{
    private readonly IOptions<AppSettings> _appSettings;
    private readonly ILogger<SendWelcomeEmailEventHandler> _logger;
    private readonly IMailService _mailService;

    public SendWelcomeEmailEventHandler(ILogger<SendWelcomeEmailEventHandler> logger, IMailService mailService,
        IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _mailService = mailService;
        _appSettings = appSettings;
    }

    public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.User.Email, $"Welcome {notification.User.FirstName}",
            $"Welcome to {_appSettings.Value.ApplicationName}! Please click here to verify your account: " + _appSettings.Value.ConfirmAccountUrl);

        return Task.CompletedTask;
    }
}
