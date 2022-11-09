using FluentValidation;

namespace Courses.Application.Features.Modules.Queries.GetShortModulesInfoByCourseId;

public class GetModulesByCourseIdValidator : AbstractValidator<ServicesContracts.Courses.Requests.Modules.Queries.GetModulesByCourseId>
{
    public GetModulesByCourseIdValidator()
    {
        RuleFor(e => e.CourseId)
            .NotNull();
        RuleFor(e => e.CourseId)
            .GreaterThan(-1).WithMessage("Module id can't be less then 0");
    }
}