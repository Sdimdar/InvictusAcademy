using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Jitsi;

public class GetByAddress : EndpointBaseAsync
    .WithRequest<GetByAddressQuery>
    .WithActionResult<DefaultResponseObject<StreamingRoomVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetByAddress(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("StreamingRooms/GetByAddress")]
    [SwaggerOperation(
        Summary = "Получить стрим комнату",
        Description = "Необходимо передать в строке запроса адрес комнаты",
        Tags = new[] { "StreamingRooms" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<StreamingRoomVm>>> HandleAsync([FromQuery]GetByAddressQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<StreamingRoomVm>>(response));
    }
}