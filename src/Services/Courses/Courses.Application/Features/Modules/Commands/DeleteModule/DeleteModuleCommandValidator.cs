using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Features.Modules.Commands.DeleteModule;

public class DeleteModuleCommandValidator : AbstractValidator<DeleteModuleCommand>
{
    public DeleteModuleCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(-1).WithMessage("Module ID can't be less then 0");
    }
}
