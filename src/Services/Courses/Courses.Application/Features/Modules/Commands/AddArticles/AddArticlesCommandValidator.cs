using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Features.Modules.Commands.AddArticles;

public class AddArticlesCommandValidator : AbstractValidator<AddArticlesCommand>
{
	public AddArticlesCommandValidator()
	{
		RuleFor(p => p.ModuleId)
			.GreaterThan(-1).WithMessage("Module Id can't be less then 0");
	}
}
