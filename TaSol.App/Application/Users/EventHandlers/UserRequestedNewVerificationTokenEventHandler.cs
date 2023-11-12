using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers;

public class
    UserRequestedNewVerificationTokenEventHandler : INotificationHandler<UserRequestedNewVerificationTokenEvent>
{
    private readonly ILogger<UserRequestedNewVerificationTokenEventHandler> _logger;

    public UserRequestedNewVerificationTokenEventHandler(ILogger<UserRequestedNewVerificationTokenEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserRequestedNewVerificationTokenEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}