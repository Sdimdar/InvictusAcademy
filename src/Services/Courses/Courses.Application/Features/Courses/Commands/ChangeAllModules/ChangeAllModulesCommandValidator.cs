using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.ChangeAllModules;

public class ChangeAllModulesCommandValidator : AbstractValidator<ChangeAllModulesCommand>
{
    public ChangeAllModulesCommandValidator()
    {
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID is can't be less 0");
        RuleForEach(p => p.ModulesId)
            .GreaterThan(-1).WithMessage("Module ID is can't be less 0");
    }
}
