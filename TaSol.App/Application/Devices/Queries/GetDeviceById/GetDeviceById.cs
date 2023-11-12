namespace Application.Queries.Queries.GetDeviceById;

public record GetDeviceByIdQuery : IRequest<GetDeviceByIdDto>
{
    // Properties go here
}

public class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, GetDeviceByIdDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDeviceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetDeviceByIdDto> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}