using Ardalis.Result;
using MediatR;
using ServicesContracts.Request.Responses;

namespace ServicesContracts.Request.Requests.Querries;

public class GetAllRequestCommand : IRequest<Result<GetAllRequestVm>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}