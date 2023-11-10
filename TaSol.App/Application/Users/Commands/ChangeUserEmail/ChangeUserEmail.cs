using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.ChangeUserEmail;

public record ChangeUserEmailCommand : IRequest<long>
{
    // Properties go here
}

public class ChangeUserEmailCommandHandler : IRequestHandler<ChangeUserEmailCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ChangeUserEmailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ChangeUserEmailCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
