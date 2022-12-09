using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.RemoveFromWished;

public class RemoveFromWishedCommandValidator : AbstractValidator<RemoveFromWishedCommand>
{
    public RemoveFromWishedCommandValidator()
    {
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
        RuleFor(p => p.UserId)
            .GreaterThan(-1).WithMessage("User ID can't be less then 0");
    }
}