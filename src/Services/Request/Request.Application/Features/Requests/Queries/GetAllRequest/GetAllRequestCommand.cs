using Ardalis.Result;
using MediatR;

namespace Request.Application.Features.Requests.Queries.GetAllRequest;

public class GetAllRequestCommand : IRequest<Result<GetAllRequestVm>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}