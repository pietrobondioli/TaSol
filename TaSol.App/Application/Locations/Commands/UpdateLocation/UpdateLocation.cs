namespace Application.Locations.Commands.UpdateLocation;

public record UpdateLocationCommand : IRequest<long>
{
    // Properties go here
}

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, long>
{
    private readonly IApplicationDbContext _context;

    public UpdateLocationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        // Handler logic goes here
        throw new NotImplementedException(); // Replace with actual return
    }
}