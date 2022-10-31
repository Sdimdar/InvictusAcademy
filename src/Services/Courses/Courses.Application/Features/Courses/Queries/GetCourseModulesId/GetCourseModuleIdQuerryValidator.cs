using FluentValidation;
using ServicesContracts.Courses.Requests.Querries;

namespace Courses.Application.Features.Courses.Queries.GetCourseModulesId;

public class GetCourseModuleIdQuerryValidator : AbstractValidator<GetCourseModulesIdQuerry>
{
    public GetCourseModuleIdQuerryValidator()
    {
        RuleFor(p => p.CourseId).GreaterThan(0).WithMessage("Course ID can't be less then 1");
    }
}
