using Ardalis.Result;
using MediatR;

namespace ServicesContracts.Jitsi.Commands;

public class CreateStreamingRoomCommand : IRequest<Result<string>>
{
    public string Name { get; set; }
    public string ImageLink { get; set; }
}