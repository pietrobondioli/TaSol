using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.VerifyUserAccount;

public record VerifyUserAccountCommand : IRequest<long>
{
    // Properties go here
}

public class VerifyUserAccountCommandHandler : IRequestHandler<VerifyUserAccountCommand, long>
{
    private readonly IApplicationDbContext _context;

    public VerifyUserAccountCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(VerifyUserAccountCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}
