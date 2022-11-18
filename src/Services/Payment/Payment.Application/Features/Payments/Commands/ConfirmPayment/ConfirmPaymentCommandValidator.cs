using FluentValidation;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.ConfirmPayment;

public class ConfirmPaymentCommandValidator : AbstractValidator<ConfirmPaymentCommand>
{
    public ConfirmPaymentCommandValidator()
    {
        RuleFor(p => p.PaymentId)
            .GreaterThan(-1).WithMessage("Payment Id can't be less then 0");
        RuleFor(p => p.AdminEmail)
            .MaximumLength(50).WithMessage("Admin email, can't be longer then 50 characters");
    }
}