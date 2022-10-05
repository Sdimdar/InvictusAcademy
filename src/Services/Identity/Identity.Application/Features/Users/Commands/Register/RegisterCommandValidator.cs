using FluentValidation;
using Identity.Application.Contracts;

namespace Identity.Application.Features.Users.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(q => q.Email)
            .MustAsync(IsUniqueEmail).WithMessage("Email already registered")
            .NotEmpty().WithMessage("Email is Required.")
            .NotNull()
            .EmailAddress().WithMessage("It's not Email address");
        RuleFor(q => q.Password)
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
        RuleFor(q => q.PasswordConfirm)
            .NotEmpty().WithMessage("Password confirmation is Required")
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
                "password must contain at least 6 symbols")
            .Must((querry, passwordConfirmation) => querry.Password == passwordConfirmation)
            .WithMessage("The password and its confirmation do not match");
        RuleFor(q => q.PhoneNumber)
            .MustAsync(IsUniqueNumber).WithMessage("PhoneNumber already registered")
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

    private async Task<bool> IsUniqueEmail(string email, CancellationToken arg2)
    {
        return await _userRepository.GetByPredicateAsync(u => u.Email == email) is null;
    }
    private async Task<bool> IsUniqueNumber(string phoneNumber, CancellationToken arg3)
    {
        return await _userRepository.GetByPredicateAsync(u => u.PhoneNumber == phoneNumber) is null;
    }
}