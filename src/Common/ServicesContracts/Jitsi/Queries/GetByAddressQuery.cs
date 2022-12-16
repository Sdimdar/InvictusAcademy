using Ardalis.Result;
using MediatR;
using ServicesContracts.Jitsi.Models;

namespace ServicesContracts.Jitsi.Queries;

public class GetByAddressQuery : IRequest<Result<StreamingRoomVm>>
{
    public string Address { get; set; }
}