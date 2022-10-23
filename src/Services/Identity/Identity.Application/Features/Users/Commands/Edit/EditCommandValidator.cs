using FluentValidation;
using ServicesContracts.Identity.Requests.Commands;

namespace Identity.Application.Features.Users.Commands.Edit;

public class EditCommandValidator : AbstractValidator<EditCommand>
{
    public EditCommandValidator()
    {
        RuleFor(q => q.FirstName)
            .NotEmpty().WithMessage("FirstName is Required")
            .NotNull()
            .MaximumLength(100).WithMessage("FirstName must be less than 100 characters long");
        RuleFor(q => q.LastName)
            .NotEmpty().WithMessage("LastName is Required")
            .NotNull()
            .MaximumLength(100).WithMessage("LastName must be less than 100 characters long");
        RuleFor(q => q.MiddleName)
            .MaximumLength(100).WithMessage("MiddleName must be less than 100 characters long");
        RuleFor(q => q.InstagramLink)
            .MaximumLength(100).WithMessage("InstagramLink must be less than 100 characters long");
        RuleFor(q => q.Citizenship)
            .NotEmpty().WithMessage("FirstName is Required")
            .NotNull()
            .MaximumLength(60).WithMessage("Citizenship must be less than 60 characters long");
        RuleFor(q => q.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is Required")
            .NotNull().WithMessage("PhoneNumber is Required")
            .MinimumLength(11).WithMessage("PhoneNumber must be longer than 11 characters long")
            .MaximumLength(13).WithMessage("PhoneNumber must be less than 13 characters long")
            .Must(phoneNumber => phoneNumber.All(Char.IsDigit)).WithMessage("PhoneNumber must contain only numbers");
    }
}