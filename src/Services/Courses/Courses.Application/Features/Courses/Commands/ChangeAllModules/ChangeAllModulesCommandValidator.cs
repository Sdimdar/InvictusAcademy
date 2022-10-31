using FluentValidation;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.ChangeAllModules;

public class ChangeAllModulesCommandValidator : AbstractValidator<ChangeAllModulesCommand>
{
    public ChangeAllModulesCommandValidator()
    {
        RuleFor(p => p.CourseId).GreaterThan(0).WithMessage("Course ID is can't be less 1");
        RuleForEach(p => p.ModulesId.Select(p => p)).GreaterThan(0).WithMessage("Module ID is can't be less 1");
    }
}
