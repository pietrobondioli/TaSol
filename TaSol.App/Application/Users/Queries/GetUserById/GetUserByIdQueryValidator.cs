namespace Application.Users.Queries.GetUserById;

public class RegisterCommandValidator : AbstractValidator<GetUserByIdQuery>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}