using Application.Devices.Commands.ChangeDeviceLocation;
using Application.Devices.Commands.CreateDevice;
using Application.Devices.Commands.DisableDevice;
using Application.Devices.Commands.EnableDevice;
using Application.Devices.Commands.RegenerateAuthToken;
using Application.Devices.Commands.UpdateDevice;
using Application.Devices.Queries.GetDeviceById;
using Application.Devices.Queries.GetDevicesWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Devices.DTOs;

namespace Web.Controllers.Devices;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DeviceController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;
    private readonly ISender _sender;

    public DeviceController(ILogger<DeviceController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetPaginatedAsync([FromQuery] int pageNumber, [FromQuery] int pageSize,
        [FromQuery] string? deviceName)
    {
        var query = new GetDevicesWithPaginationQuery
        {
            PageNumber = pageNumber, PageSize = pageSize, DeviceName = deviceName
        };

        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAsync([FromRoute] string id)
    {
        var query = new GetDeviceByIdQuery { DeviceId = long.Parse(id) };

        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateDeviceDto body)
    {
        var command = new CreateDeviceCommand
        {
            Name = body.Name, Description = body.Description ?? string.Empty, LocationId = body.LocationId
        };

        var (resulid, token) = await _sender.Send(command);

        return Ok(new { Id = resulid, Token = token });
    }

    [HttpPost("{id}/change-location")]
    public async Task<IActionResult> ChangeLocationAsync([FromRoute] string id, [FromBody] ChangeDeviceLocationDto body)
    {
        var command = new ChangeDeviceLocationCommand { DeviceId = long.Parse(id), NewLocationId = body.NewLocationId };

        var result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPost("{id}/enable")]
    public async Task<IActionResult> EnableAsync([FromRoute] string id)
    {
        var command = new EnableDeviceCommand { DeviceId = long.Parse(id) };

        var result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPost("{id}/disable")]
    public async Task<IActionResult> DisableAsync([FromRoute] string id)
    {
        var command = new DisableDeviceCommand { DeviceId = long.Parse(id) };

        var result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPost("{id}/update")]
    public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateDeviceDto body)
    {
        var command =
            new UpdateDeviceCommand { DeviceId = long.Parse(id), Name = body.Name, Description = body.Description };

        var result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpPost("{id}/regenerate-token")]
    public async Task<IActionResult> RegenerateTokenAsync([FromRoute] string id)
    {
        var command = new RegenerateAuthTokenCommand { DeviceId = long.Parse(id) };

        var (_, token) = await _sender.Send(command);

        return Ok(new { Id = id, Token = token });
    }
}
