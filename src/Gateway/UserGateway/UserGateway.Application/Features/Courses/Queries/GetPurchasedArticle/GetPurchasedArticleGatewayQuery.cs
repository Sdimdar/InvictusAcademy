using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Features.Courses.Queries.GetPurchasedArticle;

public class GetPurchasedArticleGatewayQuery : IRequest<Result<PurchasedArticleInfoVm>>
{
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public int ArticleOrder { get; set; }
}
