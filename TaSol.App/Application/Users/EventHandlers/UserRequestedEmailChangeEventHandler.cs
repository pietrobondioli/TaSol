using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Application.Users.EventHandlers;

public class UserRequestedEmailChangeEventHandler : INotificationHandler<UserRequestedEmailChangeEvent>
{
    private readonly ILogger<UserRequestedEmailChangeEventHandler> _logger;

    public UserRequestedEmailChangeEventHandler(ILogger<UserRequestedEmailChangeEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserRequestedEmailChangeEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}