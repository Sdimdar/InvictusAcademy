using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Jitsi;
using ServicesContracts.Jitsi.Models;
using ServicesContracts.Jitsi.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Jitsi;

public class GetAll : EndpointBaseAsync
    .WithRequest<GetAllRoomsQuery>
    .WithActionResult<DefaultResponseObject<AllStreamingRoomsVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAll> _logger;

    public GetAll(IMediator mediator, IMapper mapper, ILogger<GetAll> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpGet("StreamingRooms/GetAll")]
    [SwaggerOperation(
        Summary = "Взять все стриминговые комнаты",
        Description = "Необходимо передать в строке номер страницы и кол-во элементов на странице",
        Tags = new[] { "StreamingRooms" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<AllStreamingRoomsVm>>> HandleAsync([FromQuery]GetAllRoomsQuery request, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"PageNumber {request.PageNumber}" +
                               $"PageSize {request.PageSize}");
        var response = await _mediator.Send(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" +
                               $"Errors {response.Errors}" +
                               $"ValidationErrors {response.ValidationErrors}" +
                               $"IsSuccess {response.IsSuccess}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<AllStreamingRoomsVm>>(response));
    }
}