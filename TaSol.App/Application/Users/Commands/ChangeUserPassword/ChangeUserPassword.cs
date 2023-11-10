using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Users.Commands.ChangeUserPassword;

public record ChangeUserPasswordCommand : IRequest<long>
{
    // Properties go here
}

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, long>
{
    private readonly IApplicationDbContext _context;

    public ChangeUserPasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}
