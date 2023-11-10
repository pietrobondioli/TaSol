using Application.Common.Interfaces;

namespace Application.Queries.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : IRequest<GetUsersWithPaginationDto>
{
    // Properties go here
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, GetUsersWithPaginationDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetUsersWithPaginationDto> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
