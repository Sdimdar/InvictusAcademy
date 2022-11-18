using FluentValidation;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentById;

public class GetPaymentByIdQueryValidator : AbstractValidator<GetPaymentQuery>
{
    public GetPaymentByIdQueryValidator()
    {
        RuleFor(p => p.PaymentId)
            .GreaterThan(-1).WithMessage("Payment Id can't be less then 0");
    }
}