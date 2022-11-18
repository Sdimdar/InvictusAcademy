using FluentValidation;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.AddPayment;

public class AddPaymentCommandValidator : AbstractValidator<AddPaymentCommand>
{
    public AddPaymentCommandValidator()
    {
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course Id can't be less then 0");
        RuleFor(p => p.UserId)
            .GreaterThan(-1).WithMessage("User Id can't be less then 0");
    }
}