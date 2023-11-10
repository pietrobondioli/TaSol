using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers;

public class UserChangedEmailEventHandler : INotificationHandler<UserChangedEmailEvent>
{
    private readonly ILogger<UserChangedEmailEventHandler> _logger;

    public UserChangedEmailEventHandler(ILogger<UserChangedEmailEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserChangedEmailEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
