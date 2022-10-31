using FluentValidation;
using ServicesContracts.Courses.Requests.Commands;

namespace Courses.Application.Features.Courses.Commands.RemoveModule;

public class RemoveModuleCommandValidator : AbstractValidator<RemoveModuleCommand>
{
    public RemoveModuleCommandValidator()
    {
        RuleFor(p => p.ModuleId).GreaterThan(0).WithMessage("Module ID is can't be less then 0");
        RuleFor(p => p.CourseId).GreaterThan(0).WithMessage("Course ID is can't be less then 0");
    }
}
