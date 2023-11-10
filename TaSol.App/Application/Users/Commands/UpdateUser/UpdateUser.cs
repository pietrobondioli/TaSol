using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Commands.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<long>
{
    // Properties go here
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, long>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        return default; // Replace with actual return
    }
}
