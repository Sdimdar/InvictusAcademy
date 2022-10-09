using FluentValidation;

namespace Request.Application.Features.Requests.Commands.CreateRequest;

public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
    public CreateRequestCommandValidator()
    {
        RuleFor(q => q.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is Required")
            .NotNull()
            .MaximumLength(13).WithMessage("PhoneNumber must be less than 13 characters long")
            .Must(phoneNumber => phoneNumber.All(Char.IsDigit)).WithMessage("PhoneNumber must contain only numbers");
        RuleFor(q => q.UserName)
            .NotEmpty().WithMessage("UserName is Required.")
            .NotNull();
    }
}