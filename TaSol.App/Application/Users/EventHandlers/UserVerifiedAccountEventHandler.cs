using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers;

public class UserVerifiedAccountEventHandler : INotificationHandler<UserVerifiedAccountEvent>
{
    private readonly ILogger<UserVerifiedAccountEventHandler> _logger;

    public UserVerifiedAccountEventHandler(ILogger<UserVerifiedAccountEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserVerifiedAccountEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
