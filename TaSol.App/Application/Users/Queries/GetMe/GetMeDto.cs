using Domain.Entities;

namespace Application.Queries.Queries.GetMe;

public class GetMeDto
{
    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Role { get; set; }

    public bool IsVerified { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetMeDto>();
        }
    }
}
