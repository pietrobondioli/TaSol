using Domain.Entities;

namespace Application.Queries.Queries.GetLocationById;

public class GetLocationByIdDto
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string Latitude { get; set; }

    public string Longitude { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Location, GetLocationByIdDto>();
        }
    }
}
