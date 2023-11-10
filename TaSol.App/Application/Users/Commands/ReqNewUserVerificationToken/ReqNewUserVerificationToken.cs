using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Users.Commands.ReqNewUserVerificationToken;

public record ReqNewUserVerificationTokenCommand : IRequest<long>
{
    // Properties go here
}

public class ReqNewUserVerificationTokenCommandHandler : IRequestHandler<ReqNewUserVerificationTokenCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ReqNewUserVerificationTokenCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ReqNewUserVerificationTokenCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}
