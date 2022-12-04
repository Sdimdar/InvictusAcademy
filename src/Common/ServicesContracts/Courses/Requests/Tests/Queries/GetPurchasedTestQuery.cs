using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace ServicesContracts.Courses.Requests.Tests.Queries;

public class GetPurchasedTestQuery : IRequest<Result<List<PurchasedTestVm>>>
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public int ArticleOrder { get; set; }
}
