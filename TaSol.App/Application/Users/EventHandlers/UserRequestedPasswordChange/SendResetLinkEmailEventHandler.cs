using Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Application.Users.EventHandlers.UserRequestedPasswordChange;

public class SendResetLinkEmailEventHandler : INotificationHandler<UserRequestedPasswordChangeEvent>
{
    private readonly ILogger<SendResetLinkEmailEventHandler> _logger;
    private readonly IMailService _mailService;
    private readonly IOptions<AppSettings> _appSettings;

    public SendResetLinkEmailEventHandler(ILogger<SendResetLinkEmailEventHandler> logger, IMailService mailService, IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _mailService = mailService;
        _appSettings = appSettings;
    }

    public Task Handle(UserRequestedPasswordChangeEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.User.Email, "Reset Password",
            $"Click here to reset your password: {_appSettings.Value.ChangePasswordUrl}");

        return Task.CompletedTask;
    }
}
