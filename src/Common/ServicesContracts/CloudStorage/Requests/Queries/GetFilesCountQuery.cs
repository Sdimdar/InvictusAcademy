using Ardalis.Result;
using MediatR;

namespace ServicesContracts.CloudStorage.Requests.Queries;

public class GetFilesCountQuery : IRequest<Result<int>>
{
}
