using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;
    private readonly ISender _sender;

    public LocationController(ILogger<DeviceController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
}