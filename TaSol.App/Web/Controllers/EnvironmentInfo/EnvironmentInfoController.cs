using Application.EnvironmentInfos.Commands.CreateEnvironmentInfo;
using Application.EnvironmentInfos.Constants;
using Application.EnvironmentInfos.Queries.GetEnvironmentInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.EnvironmentInfo.DTOs;

namespace Web.Controllers.EnvironmentInfo;

[ApiController]
[Route("[controller]")]
public class EnvironmentInfoController : ControllerBase
{
    private readonly ILogger<EnvironmentInfoController> _logger;
    private readonly ISender _sender;

    public EnvironmentInfoController(ILogger<EnvironmentInfoController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] long locationId, [FromQuery] long? deviceId,
        [FromQuery] QueryRange.Options range, [FromQuery] QueryInterval.Options interval)
    {
        var query = new GetEnvironmentInfoQuery
        {
            LocationId = locationId, DeviceId = deviceId, Range = range, Interval = interval
        };

        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromHeader] string authHeader,
        [FromBody] CreateEnvironmentInfoDto body)
    {
        var command = new CreateEnvironmentInfoCommand
        {
            AuthToken = authHeader,
            Humidity = body.Humidity,
            Temperature = body.Temperature,
            LightLevel = body.LightLevel,
            RainLevel = body.RainLevel
        };

        var result = await _sender.Send(command);

        return Ok(result);
    }
}
