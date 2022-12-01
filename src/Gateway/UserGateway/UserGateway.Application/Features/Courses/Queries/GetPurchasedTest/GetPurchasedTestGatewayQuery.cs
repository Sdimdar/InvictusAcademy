using Ardalis.Result;
using MediatR;
using ServicesContracts.Courses.Responses;

namespace UserGateway.Application.Features.Courses.Queries.GetPurchasedTest;

public class GetPurchasedTestGatewayQuery : IRequest<Result<List<PurchasedTestVm>>>
{
    public int CourseId { get; set; }
    public int ModuleId { get; set; }
    public int ArticleOrder { get; set; }
}
