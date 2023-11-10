using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.ReqUserPasswordChange;

public record ReqUserPasswordChangeCommand : IRequest<long>
{
    // Properties go here
}

public class ReqUserPasswordChangeCommandHandler : IRequestHandler<ReqUserPasswordChangeCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ReqUserPasswordChangeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ReqUserPasswordChangeCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
