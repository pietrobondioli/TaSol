using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.CreateLocation;

public record CreateLocationCommand : IRequest<long>
{
    // Properties go here
}

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateLocationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}
