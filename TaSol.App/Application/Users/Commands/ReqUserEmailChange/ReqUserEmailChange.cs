namespace Application.Users.Commands.ReqUserEmailChange;

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
        throw new NotImplementedException(); // Replace with actual return
    }
}