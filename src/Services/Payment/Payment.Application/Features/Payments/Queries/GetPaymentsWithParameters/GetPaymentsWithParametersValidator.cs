using FluentValidation;
using ServicesContracts.Payments.Queries;

namespace Payment.Application.Features.Payments.Queries.GetPaymentsWithParameters;

public class GetPaymentsWithParametersValidator : AbstractValidator<GetPaymentsWithParametersQuery>
{
    public GetPaymentsWithParametersValidator()
    {
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course Id can't be less then 0");
    }
}