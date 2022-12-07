using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Commands;

namespace Courses.Application.Features.Articles.Commands.AddTest;

public class AddTestCommandValidator : AbstractValidator<AddTestCommand>
{
    public AddTestCommandValidator()
    {
        RuleFor(p => p.Order)
            .GreaterThan(-1).WithMessage("Order can't be less then 0");
        RuleFor(p => p.ModuleId)
            .GreaterThan(-1).WithMessage("Module Id can't be less then 0");
        RuleFor(p => p.Test)
            .NotNull();
    }
}