using Ardalis.Result;
using MediatR;
using ServicesContracts.CloudStorage.Responses;
using ServicesContracts.Request.Responses;

namespace ServicesContracts.CloudStorage.Requests.Querries;

public class GetAllFilesQuery : IRequest<Result<GetAllFilesVM>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}