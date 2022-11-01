using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetModulesByListOfId;

public class GetModulesByListOfIdQueryValidator : AbstractValidator<GetModulesByListOfIdQuery>
{
	public GetModulesByListOfIdQueryValidator()
	{
		RuleFor(e => e.ModulesId)
			.NotNull();
		RuleForEach(e => e.ModulesId)
			.GreaterThan(-1).WithMessage("Module id can't be less then 0");
	}
}
