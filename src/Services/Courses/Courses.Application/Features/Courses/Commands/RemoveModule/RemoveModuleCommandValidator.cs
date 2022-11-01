using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.RemoveModule;

public class RemoveModuleCommandValidator : AbstractValidator<RemoveModuleCommand>
{
    public RemoveModuleCommandValidator()
    {
        RuleFor(p => p.ModuleId)
            .GreaterThan(-1).WithMessage("Module ID is can't be less then 0");
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID is can't be less then 0");
    }
}
