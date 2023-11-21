using Domain.Entities;

namespace Application.Locations.Commands.CreateLocation;

public record CreateLocationCommand : IRequest<long>
{
    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string Address { get; init; } = null!;

    public string City { get; init; } = null!;

    public string State { get; init; } = null!;

    public string Country { get; init; } = null!;

    public string Latitude { get; init; } = null!;

    public string Longitude { get; init; } = null!;
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
        var entity = new Location
        {
            Name = request.Name,
            Description = request.Description,
            Address = request.Address,
            City = request.City,
            State = request.State,
            Country = request.Country,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        _context.Locations.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
