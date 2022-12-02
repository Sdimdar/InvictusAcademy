using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Querries;

namespace Courses.Application.Features.Courses.Queries.GetPurchasedCourseData;

public class GetPurchasedCourseDataQueryValidator : AbstractValidator<GetPurchasedCourseDataQuery>
{
	public GetPurchasedCourseDataQueryValidator()
	{
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
        RuleFor(p => p.UserId)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
    }
}
