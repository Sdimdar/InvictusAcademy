using Ardalis.Result;
using MediatR;
using Request.Application.Contracts;
using ServicesContracts.Request.Requests.Querries;

namespace Request.Application.Features.Requests.Queries.GetRequestsCount;

public class GetRequestsCountQuerryHandler : IRequestHandler<GetRequestsCountQuerry, Result<int>>
{
    private readonly IRequestRepository _requestRepository;

    public GetRequestsCountQuerryHandler(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<Result<int>> Handle(GetRequestsCountQuerry request, CancellationToken cancellationToken)
    {
        var result = await _requestRepository.GetRequestsCount();
        return Result.Success(result);
    }
}
