using Application.Queries.Queries.GetMe;
using Application.Users.Commands.AuthenticateUser;
using Application.Users.Commands.ChangeUserEmail;
using Application.Users.Commands.ChangeUserPassword;
using Application.Users.Commands.RegisterUser;
using Application.Users.Commands.ReqNewUserVerificationToken;
using Application.Users.Commands.ReqUserEmailChange;
using Application.Users.Commands.ReqUserPasswordChange;
using Application.Users.Commands.UpdateUser;
using Application.Users.Commands.VerifyUserAccount;
using Application.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Common.Interfaces;
using Web.Controllers.User.DTOs;

namespace Web.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly HttpContext _httpContext;
    private readonly IJwtService _jwtService;
    private readonly ILogger<UserController> _logger;
    private readonly ISender _sender;

    public UserController(ILogger<UserController> logger, ISender sender, IJwtService jwtService,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _sender = sender;
        _jwtService = jwtService;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        var command = new RegisterUserCommand
        {
            UserName = dto.UserName,
            Password = dto.Password,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber
        };

        var id = await _sender.Send(command);

        return Ok(id);
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserDto dto)
    {
        var command = new AuthenticateUserCommand
        {
            UserName = dto.UserName, Email = dto.Email, Password = dto.Password
        };

        var user = await _sender.Send(command);

        var jwtToken = _jwtService.GenerateToken(user.Id, user.UserName, user.Email, user.Role);

        _httpContext.Response.Cookies.Append("jwt", jwtToken,
            new CookieOptions
            {
                HttpOnly = true, SameSite = SameSiteMode.Strict, Expires = DateTime.UtcNow.AddDays(7)
            });

        return Ok(user);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var command = new GetMeQuery();

        var user = await _sender.Send(command);

        return Ok(user);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] long id)
    {
        var command = new GetUserByIdQuery { UserId = id };

        var user = await _sender.Send(command);

        return Ok(user);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] UpdateUserDto dto)
    {
        var command = new UpdateUserCommand
        {
            Id = id, FirstName = dto.FirstName, LastName = dto.LastName, PhoneNumber = dto.PhoneNumber
        };

        var userId = await _sender.Send(command);

        return Ok(userId);
    }

    [HttpPost("account-verification/req-token")]
    public async Task<IActionResult> RequestVerificationToken([FromBody] RequestUserVerificationTokenDto dto)
    {
        var command = new ReqNewUserVerificationTokenCommand { Email = dto.Email };

        await _sender.Send(command);

        return Accepted();
    }

    [HttpPost("account-verification/verify/{token}")]
    public async Task<IActionResult> VerifyAccount([FromRoute] string token)
    {
        var command = new VerifyUserAccountCommand { Token = token };

        var id = await _sender.Send(command);

        return Ok(id);
    }

    [HttpGet("password-change/req-token")]
    public async Task<IActionResult> RequestPasswordResetToken([FromBody] RequestPasswordResetTokenDto dto)
    {
        var command = new ReqUserPasswordChangeCommand { Email = dto.Email };

        await _sender.Send(command);

        return Accepted();
    }

    [HttpPost("password-change/change/{token}")]
    public async Task<IActionResult> ChangePassword([FromRoute] string token, [FromBody] ChangePasswordDto dto)
    {
        var command = new ChangeUserPasswordCommand
        {
            Token = token, NewPassword = dto.NewPassword, ConfirmNewPassword = dto.ConfirmNewPassword
        };

        var id = await _sender.Send(command);

        return Ok(id);
    }

    [HttpPost("email-change/req-token")]
    public async Task<IActionResult> RequestEmailChangeToken([FromBody] RequestEmailChangeTokenDto dto)
    {
        var command = new ReqUserEmailChangeCommand { Email = dto.Email };

        await _sender.Send(command);

        return Accepted();
    }

    [HttpPost("email-change/change/{token}")]
    public async Task<IActionResult> ChangeEmail([FromRoute] string token, [FromBody] ChangeEmailDto dto)
    {
        var command = new ChangeUserEmailCommand { Token = token, NewEmail = dto.NewEmail };

        var id = await _sender.Send(command);

        return Ok(id);
    }
}
