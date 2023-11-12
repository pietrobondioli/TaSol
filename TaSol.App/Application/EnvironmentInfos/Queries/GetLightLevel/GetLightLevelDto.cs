using Domain.Entities;

namespace Application.Queries.Queries.GetLightLevel;

public class GetLightLevelDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetLightLevelDto>(); // Adjust the source entity as needed
        }
    }
}