using FluentValidation;
using ServicesContracts.Identity.Requests.Queries;

namespace User.Application.Features.Users.Queries.GetUsersData;

public class GetUsersDataQueryValidator : AbstractValidator<GetUsersDataQuery>
{
    public GetUsersDataQueryValidator()
    {
        RuleFor(p => p.Page)
            .GreaterThan(0).WithMessage("Page number can't be less then 1");
        RuleFor(p => p.PageSize)
            .GreaterThan(-1).WithMessage("Page size can't be less then 0");
    }
}