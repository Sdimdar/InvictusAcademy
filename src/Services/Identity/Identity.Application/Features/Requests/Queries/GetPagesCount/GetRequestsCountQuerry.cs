using Ardalis.Result;
using MediatR;

namespace Identity.Application.Features.Requests.Queries.GetPagesCount;

public class GetRequestsCountQuerry : IRequest<Result<int>>
{
}
