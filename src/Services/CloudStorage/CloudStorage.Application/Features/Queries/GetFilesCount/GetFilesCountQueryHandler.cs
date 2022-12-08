using Ardalis.Result;
using CloudStorage.Application.Contracts;
using MediatR;
using ServicesContracts.CloudStorage.Requests.Queries;

namespace CloudStorage.Application.Features.Queries.GetFilesCount;

public class GetFilesCountQueryHandler : IRequestHandler<GetFilesCountQuery, Result<int>>
{
    private readonly ICloudStorageRepository _cloudStorage;

    public GetFilesCountQueryHandler(ICloudStorageRepository cloudStorage)
    {
        _cloudStorage = cloudStorage;
    }
    public async Task<Result<int>> Handle(GetFilesCountQuery request, CancellationToken cancellationToken)
    {
        var result = await _cloudStorage.GetCountAsync();
        return Result.Success(result);
    }
}