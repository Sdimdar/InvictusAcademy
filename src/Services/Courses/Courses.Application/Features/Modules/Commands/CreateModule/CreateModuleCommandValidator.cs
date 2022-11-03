using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Features.Modules.Commands.CreateModule;

public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
{
    public CreateModuleCommandValidator()
    {
        RuleFor(p => p.Title)
            .MinimumLength(5).WithMessage("Module title can't be less then 5 simbols")
            .MaximumLength(50).WithMessage("Module title can't be longer then 50 simbols")
            .NotNull();
        RuleFor(p => p.ShortDescription)
            .MinimumLength(5).WithMessage("Module description can't be less then 5 simbols")
            .MaximumLength(100).WithMessage("Module description can't be longer then 100 simbols")
            .NotNull();
    }
}
