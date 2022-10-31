using FluentValidation;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.InsertModules;

public class InsertModulesCommandValidator : AbstractValidator<InsertModulesCommand>
{
    public InsertModulesCommandValidator()
    {
        RuleFor(p => p.CourseId).GreaterThan(0).WithMessage("Course ID is can't be less 1");
        RuleForEach(p => p.ModulesId.Select(p => p)).GreaterThan(0).WithMessage("Module ID is can't be less 1");
    }
}
