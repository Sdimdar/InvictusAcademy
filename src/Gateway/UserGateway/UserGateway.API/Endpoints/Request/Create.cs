using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.Request;

public class Create : EndpointBaseAsync
    .WithRequest<CreateRequestCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<Create> _logger;

    public Create(IMediator mediator, IMapper mapper, ILogger<Create> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("Request/Create")]
    [SwaggerOperation(
        Summary = "Создание заявки пользователем",
        Description = "Для оформления заявки необходимо ввести телефон и имя",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody] CreateRequestCommand request,
                                                                                  CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"PhoneNumber {request.PhoneNumber}" +
                               $"UserName {request.UserName}");
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}
