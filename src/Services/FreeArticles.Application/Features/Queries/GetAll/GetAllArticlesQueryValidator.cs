using FluentValidation;
using ServicesContracts.FreeArticles.Queries;

namespace FreeArticles.Application.Features.Queries.GetAll;

public class GetAllArticlesQueryValidator : AbstractValidator<GetAllFreeArticlesQuery>
{
    public GetAllArticlesQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThan(0).WithMessage("Page number can't be less then 1");
        RuleFor(p => p.PageSize)
            .GreaterThan(-1).WithMessage("Page size can't be less then 0");
    }
}