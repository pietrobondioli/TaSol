namespace Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;

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
        throw new NotImplementedException(); // Replace with actual return
    }
}