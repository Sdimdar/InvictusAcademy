using FluentValidation;

namespace Courses.Application.Features.Courses.Queries.GetCourseById;

public class GetCoursByIdQueryValidator : AbstractValidator<GetCoursByIdQuery>
{
    public GetCoursByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
    }
}
