﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.Jitsi;

public class GetAll : EndpointBaseAsync
    .WithRequest<GetAllRoomsQuery>
    .WithActionResult<DefaultResponseObject<AllStreamingRoomsVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAll(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("StreamingRooms/GetAll")]
    [SwaggerOperation(
        Summary = "Взять все стриминговые комнаты",
        Description = "Необходимо передать в строке номер страницы и кол-во элементов на странице",
        Tags = new[] { "StreamingRooms" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<AllStreamingRoomsVm>>> HandleAsync([FromQuery]GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var email = HttpContext.Session.GetData("user").Email;
        if (email is null)
        {
            throw new UnauthorizedAccessException("User is not authorized");
        }
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<AllStreamingRoomsVm>>(response));
    }
}