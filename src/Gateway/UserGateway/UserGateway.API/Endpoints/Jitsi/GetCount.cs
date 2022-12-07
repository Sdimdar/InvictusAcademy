using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Jitsi;

public class GetCount : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<int>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetCount(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("StreamingRooms/GetCount")]
    [SwaggerOperation(
        Summary = "Взять количество комнат",
        Description = "",
        Tags = new[] { "StreamingRooms" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<int>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(new GetCountRoomsQuery(), cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<int>>(response));
    }
}