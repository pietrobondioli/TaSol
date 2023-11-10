using Domain.Entities;

namespace Application.Queries.Queries.GetMe;

public class GetMeDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetMeDto>(); // Adjust the source entity as needed
        }
    }
}
