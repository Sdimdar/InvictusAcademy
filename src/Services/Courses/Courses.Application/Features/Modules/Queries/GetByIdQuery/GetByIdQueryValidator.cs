using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetByIdQuery;

public class GetByIdQueryValidator : AbstractValidator<GetModuleByIdQuery>
{
	public GetByIdQueryValidator()
	{
		RuleFor(e => e.Id)
			.GreaterThan(-1).WithMessage("Module Id, can't be less then 0");
	}
}
