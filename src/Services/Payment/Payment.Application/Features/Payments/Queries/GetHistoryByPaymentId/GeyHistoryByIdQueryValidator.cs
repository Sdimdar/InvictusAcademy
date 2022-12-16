using FluentValidation;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetHistoryByPaymentId;

public class GeyHistoryByIdQueryValidator:AbstractValidator<GetHistoryByPaymentIdQuery>
{
    public GeyHistoryByIdQueryValidator()
    {
        RuleFor(p => p.PaymentId)
            .GreaterThan(-1).WithMessage("Payment Id can't be less then 0");
    }
}