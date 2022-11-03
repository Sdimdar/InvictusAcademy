using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Commands;

namespace Courses.Application.Features.Courses.Commands.Delete;

public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
	public DeleteCourseCommandValidator()
	{
		RuleFor(e => e.Id)
			.GreaterThan(-1).WithMessage("Course ID can't be less then 0");
	}
}
