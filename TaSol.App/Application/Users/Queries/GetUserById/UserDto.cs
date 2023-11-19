using Domain.Entities;

namespace Application.Users.Queries.GetUserById;

public class UserDto
{
    public long Id { get; init; }

    public string UserName { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string PhoneNumber { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
