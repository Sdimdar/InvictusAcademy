using FluentValidation;
using ServicesContracts.FreeArticles.Commands;

namespace FreeArticles.Application.Features.Commands.Edit;

public class EditFreeArticleCommandValidator : AbstractValidator<EditFreeArticleCommand>
{
    public EditFreeArticleCommandValidator()
    {
        RuleFor(q => q.Title)
            .NotEmpty().WithMessage("Title is required")
            .NotNull()
            .MaximumLength(100);
        RuleFor(q => q.Text)
            .NotEmpty().WithMessage("Text is required")
            .NotNull();
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotNull()
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}