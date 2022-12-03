using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.InsertModule;

public class InsertModuleCommandValidator : AbstractValidator<InsertModuleCommand>
{
    public InsertModuleCommandValidator()
    {
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID is can't be less 0");
        RuleFor(p => p.ModuleId)
            .GreaterThan(-1).WithMessage("Module ID is can't be less 0");
    }
}
