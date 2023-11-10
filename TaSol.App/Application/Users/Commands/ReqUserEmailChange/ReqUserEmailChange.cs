using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.ReqUserEmailChange;

public record ReqUserEmailChangeCommand : IRequest<long>
{
    // Properties go here
}

public class ReqUserEmailChangeCommandHandler : IRequestHandler<ReqUserEmailChangeCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ReqUserEmailChangeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ReqUserEmailChangeCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
