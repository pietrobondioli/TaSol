using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger _logger;
    private readonly IUser _user;

    public LoggingBehaviour(ILogger<TRequest> logger, IApplicationDbContext context, IUserFactory userFactory)
    {
        _logger = logger;
        _context = context;
        _user = userFactory.CreateUser();
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _user.Id ?? default;
        var userName = string.Empty;

        if (!userId.Equals(default))
        {
            userName = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync(cancellationToken);
        }

        _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
