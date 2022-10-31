using FluentValidation;
using ServicesContracts.Request.Requests.Querries;

namespace Request.Application.Features.Requests.Queries.GetAllRequest;

public class GetAllRequestQueryValidator : AbstractValidator<GetAllRequestsQuery>
{
    public GetAllRequestQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThan(0).WithMessage("Page number can't be less then 1");
        RuleFor(p => p.PageSize)
            .GreaterThan(-1).WithMessage("Page size can't be less then 0");
    }
}