using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.InsertModules;

public class InsertModulesCommandValidator : AbstractValidator<InsertModulesCommand>
{
    public InsertModulesCommandValidator()
    {
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID is can't be less 0");
        RuleForEach(p => p.ModulesId)
            .GreaterThan(-1).WithMessage("Module ID is can't be less 0");
    }
}
