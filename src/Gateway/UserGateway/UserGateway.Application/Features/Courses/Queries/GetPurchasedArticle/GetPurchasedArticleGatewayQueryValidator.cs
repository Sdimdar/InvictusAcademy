using FluentValidation;
using ServicesContracts.Courses.Requests.Courses.Querries;

namespace UserGateway.Application.Features.Courses.Queries.GetPurchasedArticle;

public class GetPurchasedArticleGatewayQueryValidator : AbstractValidator<GetPurchasedArticleQuery>
{
    public GetPurchasedArticleGatewayQueryValidator()
    {
        RuleFor(p => p.ArticleOrder)
            .GreaterThan(-1).WithMessage("Article order can't be less then 0");
        RuleFor(p => p.ModuleId)
            .GreaterThan(-1).WithMessage("Module ID can't be less then 0");
        RuleFor(p => p.CourseId)
            .GreaterThan(-1).WithMessage("Course ID can't be less then 0");
    }
}
