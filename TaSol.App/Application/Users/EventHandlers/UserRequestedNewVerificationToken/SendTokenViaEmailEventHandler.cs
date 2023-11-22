using Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Settings;

namespace Application.Users.EventHandlers.UserRequestedNewVerificationToken;

public class SendTokenViaEmailEventHandler : INotificationHandler<UserRequestedNewVerificationTokenEvent>
{
    private readonly IOptions<AppSettings> _appSettings;
    private readonly ILogger<SendTokenViaEmailEventHandler> _logger;
    private readonly IMailService _mailService;

    public SendTokenViaEmailEventHandler(ILogger<SendTokenViaEmailEventHandler> logger, IMailService mailService,
        IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _mailService = mailService;
        _appSettings = appSettings;
    }

    public Task Handle(UserRequestedNewVerificationTokenEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        _mailService.SendAsync(notification.User.Email, $"Verify your account",
            $"Please click here to verify your account: " + _appSettings.Value.ConfirmAccountUrl);

        return Task.CompletedTask;
    }
}
