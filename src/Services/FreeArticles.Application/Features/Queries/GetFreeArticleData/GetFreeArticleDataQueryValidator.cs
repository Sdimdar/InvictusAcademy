using FluentValidation;
using ServicesContracts.FreeArticles.Queries;

namespace FreeArticles.Application.Features.Queries.GetFreeArticleData;

public class GetFreeArticleDataQueryValidator : AbstractValidator<GetFreeArticleDataQuery>
{
    public GetFreeArticleDataQueryValidator()
    {
        RuleFor(q => q.Id)
            .NotEmpty().WithMessage("Id is required")
            .NotNull()
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}