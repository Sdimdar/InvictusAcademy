﻿using Ardalis.Result;
using AutoMapper;
using MediatR;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using UserGateway.Application.Contracts;

namespace UserGateway.Application.Features.StreamingRoom.Queries;

public class GetByAddressQueryHandler : IRequestHandler<GetByAddressQuery, Result<StreamingRoomVm>>
{
    private readonly IStreamingRoomService _streamingRoomService;
    private readonly IMapper _mapper;

    public GetByAddressQueryHandler(IStreamingRoomService streamingRoomService, IMapper mapper)
    {
        _streamingRoomService = streamingRoomService;
        _mapper = mapper;
    }

    public async Task<Result<StreamingRoomVm>> Handle(GetByAddressQuery request, CancellationToken cancellationToken)
    {
        var response =await _streamingRoomService.GetCount(cancellationToken);
        // return Result.Success(_mapper.Map<Result<StreamingRoomVm>>(response));
        return Result.Success(_mapper.Map<StreamingRoomVm>(response));
    }
}