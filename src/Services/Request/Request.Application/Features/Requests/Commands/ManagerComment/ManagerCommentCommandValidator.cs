using FluentValidation;

namespace Request.Application.Features.Requests.Commands.ManagerComment;

public class ManagerCommentCommandValidator:AbstractValidator<ManagerCommentCommand>
{
    public ManagerCommentCommandValidator()
    {
        RuleFor(e => e.ManagerComment)
            .MaximumLength(100).WithMessage("Maximum 100 symbols");
    }
}