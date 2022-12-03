using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Courses.Querries;

public class GetPurchasedArticleQuery : IRequest<Result<PurchasedArticleInfoVm>>
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public int ArticleOrder { get; set; }
}
