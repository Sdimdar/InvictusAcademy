using Ardalis.Result;
using MediatR;
using ServicesContracts.Jitsi.Models;

namespace ServicesContracts.Jitsi.Queries;

public class GetAllRoomsQuery : IRequest<Result<AllStreamingRoomsVm>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}