using FluentValidation;
using Identity.Application.Contracts;
using Identity.Application.Features.Users.Commands.Register;

namespace Identity.Application.Features.Users.Commands.Edit;

public class EditCommandValidator : AbstractValidator<EditCommand>
{
    public EditCommandValidator()
    {
        RuleFor(q => q.MiddleName)
            .MaximumLength(100).WithMessage("MiddleName must be less than 100 characters long");
        RuleFor(q => q.InstagramLink)
            .MaximumLength(100).WithMessage("InstagramLink must be less than 100 characters long");
        RuleFor(q => q.Citizenship)
            .MaximumLength(60).WithMessage("Citizenship must be less than 60 characters long");
    }
}