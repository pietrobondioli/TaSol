using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Location : BaseAuditableEntity
{
    [Required] public string Name { get; set; } = null!;

    [Required] public string Description { get; set; } = null!;

    [Required] public string Address { get; set; } = null!;

    [Required] public string City { get; set; } = null!;

    [Required] public string State { get; set; } = null!;

    [Required] public string Country { get; set; } = null!;

    [Required] public string Latitude { get; set; } = null!;

    [Required] public string Longitude { get; set; } = null!;
}
