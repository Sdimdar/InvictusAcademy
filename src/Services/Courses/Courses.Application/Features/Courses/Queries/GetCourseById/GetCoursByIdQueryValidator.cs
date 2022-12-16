using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Querries;

namespace Courses.Application.Features.Courses.Queries.GetCourseById;

public class GetCoursByIdQueryValidator : AbstractValidator<GetCourseByIdQuery>
{
    public GetCoursByIdQueryValidator()
    {
        RuleFor(p => p.Id)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
    }
}
