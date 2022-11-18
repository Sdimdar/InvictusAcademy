using FluentValidation;
using ServicesContracts.Payments.Commands;

namespace Payment.Application.Features.Payments.Commands.RejectPayment;

public class RejectPaymentCommandValidator : AbstractValidator<RejectPaymentCommand>
{
    public RejectPaymentCommandValidator()
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