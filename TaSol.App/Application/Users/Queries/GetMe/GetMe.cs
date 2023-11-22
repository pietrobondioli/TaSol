using Application.Common.Security;
using Domain.Entities;

namespace Application.Queries.Queries.GetMe;

[Authorize]
public record GetMeQuery : IRequest<GetMeDto>
{
}

public class GetMeQueryHandler : IRequestHandler<GetMeQuery, GetMeDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public GetMeQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<GetMeDto> Handle(GetMeQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Id == _user.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), _user.Id);
        }

        return _mapper.Map<GetMeDto>(user);
    }
}
