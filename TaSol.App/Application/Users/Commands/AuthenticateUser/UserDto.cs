using Domain.Entities;

namespace Application.Users.Commands.AuthenticateUser;

public class UserDto
{
    public string Id { get; init; } = string.Empty;
    
    public string UserName { get; init; } = string.Empty;
    
    public string Email { get; init; } = string.Empty;
    
    public string Role { get; init; } = string.Empty;
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}