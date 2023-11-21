namespace Web.Controllers.User.DTOs;

public class ChangePasswordDto
{
    public string NewPassword { get; set; } = null!;

    public string ConfirmNewPassword { get; set; } = null!;
}
