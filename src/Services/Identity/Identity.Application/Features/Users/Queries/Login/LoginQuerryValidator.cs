using FluentValidation;

namespace Identity.Application.Features.Users.Queries.Login;

public class LoginQuerryValidator : AbstractValidator<LoginQuerry>
{
    public LoginQuerryValidator()
    {
        RuleFor(q => q.Email)
            .NotEmpty().WithMessage("Email is Required.")
            .NotNull()
            .EmailAddress().WithMessage("It's not Email address");
        RuleFor(q => q.Password)
            .NotEmpty().WithMessage("Password is Required")
            .NotNull()
            .Length(6,30).WithMessage("Password must be more than 6 characters long and less than 30 characters long");
    }
}
