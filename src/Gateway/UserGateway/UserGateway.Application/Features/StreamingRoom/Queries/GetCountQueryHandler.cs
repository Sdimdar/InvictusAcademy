using Ardalis.Result;
using AutoMapper;
using MediatR;
using ServicesContracts.Jitsi.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.StreamingRoom.Queries;

public class GetCountQueryHandler : IRequestHandler<GetCountRoomsQuery, Result<int>>
{
    private readonly IStreamingRoomService _streamingRoomService;
    private readonly IMapper _mapper;

    public GetCountQueryHandler(IStreamingRoomService streamingRoomService, IMapper mapper)
    {
        _streamingRoomService = streamingRoomService;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(GetCountRoomsQuery request, CancellationToken cancellationToken)
    {
        var response = await _streamingRoomService.GetCount(cancellationToken);
        if (response.IsSuccess)
        {
            return Result.Success(response.Value);
        }
        return Result.Error(response.Errors);
    }
}