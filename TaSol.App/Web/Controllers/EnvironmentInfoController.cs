using Application.Queries.Queries.GetEnvironmentInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> GetAsync()
    {
        var query = new GetEnvironmentInfoQuery();

        var result = await _sender.Send(query);

        return Ok(result);
    }
}
