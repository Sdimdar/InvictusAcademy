using FluentValidation;
using ServicesContracts.Request.Requests.Commands;

namespace Request.Application.Features.Requests.Commands.ManagerComment;

public class ManagerCommentCommandValidator : AbstractValidator<ManagerCommentCommand>
{
    public ManagerCommentCommandValidator()
    {
        RuleFor(e => e.ManagerComment)
            .MaximumLength(100).WithMessage("Maximum comment length - 100 symbols");
    }
}