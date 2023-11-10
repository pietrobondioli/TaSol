using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.DeleteLocation;

public record DeleteLocationCommand : IRequest<long>
{
    // Properties go here
}

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, long>
{
    private readonly IApplicationDbContext _context;

    public DeleteLocationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
