using FluentValidation;
using ServicesContracts.Courses.Requests.Tests.Commands;

namespace Courses.Application.Features.Articles.Commands.CheckTestAnswers;

public class CheckTestAnswersCommandValidator : AbstractValidator<CheckTestAnswersCommand>
{
	public CheckTestAnswersCommandValidator()
	{
        RuleFor(p => p.UserId)
            .GreaterThan(-1).WithMessage("User ID can't be less then 0");
        RuleFor(p => p.ArticleOrder)
            .GreaterThan(-1).WithMessage("Article order can't be less then 0");
        RuleFor(p => p.ModuleId)
            .GreaterThan(-1).WithMessage("Module ID can't be less then 0");
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
        RuleFor(p => p.Answers)
            .NotEmpty().WithMessage("Answers can't be empty");
    }
}
