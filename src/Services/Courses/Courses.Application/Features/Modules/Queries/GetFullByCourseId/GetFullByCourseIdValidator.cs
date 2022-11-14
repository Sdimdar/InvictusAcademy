using FluentValidation;
using ServicesContracts.Courses.Requests.Modules.Queries;

namespace Courses.Application.Features.Modules.Queries.GetFullByCourseId;

public class GetFullByCourseIdValidator : AbstractValidator<GetFullByCourseIdQuery>
{
    public GetFullByCourseIdValidator()
    {
        RuleFor(e => e.CourseId)
            .NotNull();
        RuleFor(e => e.CourseId)
            .GreaterThan(-1).WithMessage("Module id can't be less then 0");
        RuleFor(e => e.UserId)
            .NotNull();
    }
}