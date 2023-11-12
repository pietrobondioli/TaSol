namespace Application.Queries.Queries.GetMe;

public record GetMeQuery : IRequest<GetMeDto>
{
    // Properties go here
}

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, GetMeDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetMeDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}