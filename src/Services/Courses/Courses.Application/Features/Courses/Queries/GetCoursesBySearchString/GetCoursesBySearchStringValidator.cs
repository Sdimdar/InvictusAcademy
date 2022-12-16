using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Querries;

namespace Courses.Application.Features.Courses.Queries.GetCoursesBySearchString;

public class GetCoursesBySearchStringValidator : AbstractValidator<GetCoursesBySearchStringCommand>
{
    public GetCoursesBySearchStringValidator()
    {
        RuleFor(p => p.SearchString)
            .NotEmpty().WithMessage("Search string cant be empty")
            .NotNull().WithMessage("Search string cant be null")
            .MinimumLength(5).WithMessage("Search string lenght can't be less then 5 symbols");
    }
}