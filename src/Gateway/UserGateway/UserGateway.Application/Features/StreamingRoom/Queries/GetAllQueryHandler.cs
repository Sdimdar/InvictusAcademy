using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.StreamingRoom.Queries;

public class GetAllQueryHandler : IRequestHandler<GetAllRoomsQuery, Result<AllStreamingRoomsVm>>
{
    private readonly IStreamingRoomService _streamingRoomService;
    private readonly IMapper _mapper;

    public GetAllQueryHandler(IStreamingRoomService streamingRoomService, IMapper mapper)
    {
        _streamingRoomService = streamingRoomService;
        _mapper = mapper;
    }

    public async Task<Result<AllStreamingRoomsVm>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var streamingRooms = await _streamingRoomService.GetAll(request, cancellationToken);
        return Result.Success(_mapper.Map<Result<AllStreamingRoomsVm>>(streamingRooms));
        // return Result.Success(_mapper.Map<AllStreamingRoomsVm>(streamingRooms));
        // return Result.Success(streamingRooms.Result.Value);
        // return Result.Success(streamingRooms);
    }
}