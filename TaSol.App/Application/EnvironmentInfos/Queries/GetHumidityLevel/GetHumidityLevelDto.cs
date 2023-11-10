using Domain.Entities;

namespace Application.Queries.Queries.GetHumidityLevel;

public class GetHumidityLevelDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetHumidityLevelDto>(); // Adjust the source entity as needed
        }
    }
}
