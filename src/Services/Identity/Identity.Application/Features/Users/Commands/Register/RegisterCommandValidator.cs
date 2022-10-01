using FluentValidation;

namespace Identity.Application.Features.Users.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
	public RegisterCommandValidator()
	{
        RuleFor(q => q.Email)
            .NotEmpty().WithMessage("Email is Required.")
            .NotNull()
            .EmailAddress().WithMessage("It's not Email address");
        RuleFor(q => q.Password)
            .NotEmpty().WithMessage("Password is Required")
            .NotNull()
            .Length(6, 30).WithMessage("Password must be more than 6 characters long and less than 30 characters long");
        RuleFor(q => q.PasswordConfirm)
            .NotEmpty().WithMessage("Password confirmation is Required")
            .NotNull()
            .Length(6, 30).WithMessage("Password must be more than 6 characters long and less than 30 characters long")
            .Must((querry, passwordConfirmation) => querry.Password == passwordConfirmation).WithMessage("The password and its confirmation do not match");
        RuleFor(q => q.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is Required")
            .NotNull()
            .MaximumLength(13).WithMessage("PhoneNumber must be less than 13 characters long")
            .Must(phoneNumber => phoneNumber.All(Char.IsDigit)).WithMessage("PhoneNumber must contain only numbers");
        RuleFor(q => q.FirstName)
            .NotEmpty().WithMessage("FirstName is Required")
            .NotNull()
            .MaximumLength(100).WithMessage("FirstName must be less than 100 characters long");
        RuleFor(q => q.MiddleName)
            .MaximumLength(100).WithMessage("MiddleName must be less than 100 characters long");
        RuleFor(q => q.LastName)
            .NotEmpty().WithMessage("LastName is Required")
            .NotNull()
            .MaximumLength(100).WithMessage("LastName must be less than 100 characters long");
        RuleFor(q => q.InstagramLink)
            .MaximumLength(100).WithMessage("InstagramLink must be less than 100 characters long");
        RuleFor(q => q.Citizenship)
            .NotEmpty().WithMessage("Citizenship is Required")
            .NotNull()
            .MaximumLength(60).WithMessage("Citizenship must be less than 60 characters long");
    }
}
