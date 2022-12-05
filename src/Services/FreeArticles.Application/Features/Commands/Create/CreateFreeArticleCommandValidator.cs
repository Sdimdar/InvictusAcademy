using FluentValidation;
using ServicesContracts.FreeArticles.Commands;

namespace FreeArticles.Application.Features.Commands.Create;

public class CreateFreeArticleCommandValidator : AbstractValidator<CreateFreeArticleCommand>
{
    public CreateFreeArticleCommandValidator()
    {
        RuleFor(q => q.Title)
            .NotEmpty().WithMessage("Title is required")
            .NotNull()
            .MaximumLength(100);
        RuleFor(q => q.Text)
            .NotEmpty().WithMessage("Text is required")
            .NotNull();
    }
}