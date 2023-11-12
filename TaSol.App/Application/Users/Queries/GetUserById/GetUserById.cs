namespace Application.Users.Queries.GetUserById;

public record GetUserByIdQuery : IRequest<UserDto>
{
    public long UserId { get; init; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}