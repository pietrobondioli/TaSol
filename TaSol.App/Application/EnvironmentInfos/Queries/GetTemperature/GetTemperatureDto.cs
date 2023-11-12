using Domain.Entities;

namespace Application.Queries.Queries.GetTemperature;

public class GetTemperatureDto
{
    // DTO properties go here

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, GetTemperatureDto>(); // Adjust the source entity as needed
        }
    }
}