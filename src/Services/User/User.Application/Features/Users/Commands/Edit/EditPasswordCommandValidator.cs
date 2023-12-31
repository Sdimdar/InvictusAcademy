using FluentValidation;
using ServicesContracts.Identity.Requests.Commands;

namespace User.Application.Features.Users.Commands.Edit;

public class EditPasswordCommandValidator : AbstractValidator<EditPasswordCommand>
{
    public EditPasswordCommandValidator()
    {
        RuleFor(q => q.NewPassword)
            .NotEmpty().WithMessage("Password is Required")
            .NotNull()
            .Matches("[A-Z]+").WithMessage(
                "password must contain at least 1 uppercase letter")
            .Matches("[a-z]+").WithMessage(
                "password must contain at least 1 lowercase letter")
            .Matches("[0-9]+").WithMessage(
                "password must contain at least 1 number")
            .Matches("[#?!@$_%^&*-]").WithMessage(
                "password must contain at least 1 spec character")
            .MinimumLength(6).WithMessage(
                "password must contain at least 6 symbols");
    }
}