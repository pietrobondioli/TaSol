using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.CreateEnvironmentInfo;

public record CreateEnvironmentInfoCommand : IRequest<long>
{
    // Properties go here
}

public class CreateEnvironmentInfoCommandHandler : IRequestHandler<CreateEnvironmentInfoCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateEnvironmentInfoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateEnvironmentInfoCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
