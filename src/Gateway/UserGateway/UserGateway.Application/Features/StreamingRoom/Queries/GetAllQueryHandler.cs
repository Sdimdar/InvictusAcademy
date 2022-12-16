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
        var response = await _streamingRoomService.GetAll(request, cancellationToken);
        if (response.IsSuccess)
        {
            return Result.Success(response.Value);
        }
        return Result.Error(response.Errors);
    }
}