using FluentValidation;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentsWithParameters;

public class GetPaymentsWithParametersValidator : AbstractValidator<GetPaymentsWithParametersQuery>
{
    public GetPaymentsWithParametersValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThan(0).WithMessage("Page number can't be less then 1");
        RuleFor(p => p.PageSize)
            .GreaterThan(-1).WithMessage("Page size can't be less then 0");
    }
}