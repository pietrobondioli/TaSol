using Application.Locations.Commands.CreateLocation;
using Application.Queries.Queries.GetLocationById;
using Application.Queries.Queries.GetLocationsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Location.DTOs;

namespace Web.Controllers.Location;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly ISender _sender;

    public LocationController(ILogger<LocationController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPaginatedAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string searchTerm)
    {
        var query = new GetLocationsWithPaginationQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm
        };

        var result = await _sender.Send(query);

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] string id)
    {
        var query = new GetLocationByIdQuery
        {
            LocationId = int.Parse(id)
        };

        var result = await _sender.Send(query);

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateLocationDto body)
    {
        var command = new CreateLocationCommand
        {
            Name = body.Name,
            Description = body.Description,
            Address = body.Address,
            City = body.City,
            State = body.State,
            Country = body.Country,
            Latitude = body.Latitude,
            Longitude = body.Longitude
        };

        var result = await _sender.Send(command);

        return Ok(result);
    }
}