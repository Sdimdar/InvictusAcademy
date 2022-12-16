using FluentValidation;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.CancelPayment;

public class CancelPaymentCommandValidator : AbstractValidator<CancelPaymentCommand>
{
    public CancelPaymentCommandValidator()
    {
        RuleFor(p => p.PaymentId)
            .GreaterThan(-1).WithMessage("Payment Id can't be less then 0");
        RuleFor(p => p.AdminEmail)
            .NotNull()
            .NotEmpty()
            .MaximumLength(50).WithMessage("Admin email, can't be longer then 50 characters");
        RuleFor(p => p.RejectReason)
            .NotNull()
            .NotEmpty()
            .MaximumLength(150).WithMessage("Reject reason, can't be longer then 150 characters");
    }
}