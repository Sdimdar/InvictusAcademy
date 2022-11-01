using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetModulesByFilterString;

public class GetModulesByFilterStringQueryValidator : AbstractValidator<GetModulesByFilterStringQuery>
{
    public GetModulesByFilterStringQueryValidator()
    {
        RuleFor(e => e.FilterString)
            .NotNull().WithMessage("Filter string can't be null");
    }
}