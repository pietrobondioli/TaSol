using Domain.Entities;

namespace Application.Queries.Queries.GetRainLevel;

public class GetRainLevelDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetRainLevelDto>(); // Adjust the source entity as needed
        }
    }
}