using Ardalis.Result;
using MediatR;

namespace Request.Application.Features.Requests.Queries.GetPagesCount;

public class GetRequestsCountQuerry : IRequest<Result<int>>
{
}
