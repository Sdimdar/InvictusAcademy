using FluentValidation;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.InsertModule;

public class InsertModuleCommandValidator : AbstractValidator<InsertModuleCommand>
{
	public InsertModuleCommandValidator()
	{
		RuleFor(p => p.CourseId).GreaterThan(0).WithMessage("Course ID is can't be less 1");
		RuleFor(p => p.ModuleId).GreaterThan(0).WithMessage("Module ID is can't be less 1");
	}
}
