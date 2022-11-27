using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Purchase;

public class PurchaseCourseCommandValidator : AbstractValidator<PurchaseCourseCommand>
{
    public PurchaseCourseCommandValidator()
    {
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
        RuleFor(p => p.UserId)
            .GreaterThan(-1).WithMessage("User ID can't be less then 0");
    }
}