using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.AuthenticateUser;

public record AuthenticateUserCommand : IRequest<long>
{
    // Properties go here
}

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, long>
{
    private readonly IApplicationDbContext _context;

    public AuthenticateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}
