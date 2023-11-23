using Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Application.Users.EventHandlers.UserRequestedEmailChange;

public class SendResetLinkEmailEventHandler : INotificationHandler<UserRequestedEmailChangeEvent>
{
    private readonly AppSettings _appSettings;
    private readonly ILogger<SendResetLinkEmailEventHandler> _logger;
    private readonly IMailService _mailService;

    public SendResetLinkEmailEventHandler(ILogger<SendResetLinkEmailEventHandler> logger, IMailService mailService,
       IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _mailService = mailService;
        _appSettings = appSettings.Value;
    }

    public Task Handle(UserRequestedEmailChangeEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.User.Email, $"Email Change Requested",
            $"Please click here to change your email: " + _appSettings.ChangeEmailUrl);

        return Task.CompletedTask;
    }
}
