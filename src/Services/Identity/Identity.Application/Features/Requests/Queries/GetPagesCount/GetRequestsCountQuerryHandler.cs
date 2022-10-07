using Ardalis.Result;
using Identity.Application.Contracts;
using MediatR;

namespace Identity.Application.Features.Requests.Queries.GetPagesCount;

public class GetRequestsCountQuerryHandler : IRequestHandler<GetRequestsCountQuerry, Result<int>>
{
    private readonly IRequestRepository _requestRepository;

    public GetRequestsCountQuerryHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<Result<int>> Handle(GetRequestsCountQuerry request, CancellationToken cancellationToken)
    {
        var result = await _requestRepository.GetAllRequestsAsync();
        return Result.Success(result.Count());
    }
}
