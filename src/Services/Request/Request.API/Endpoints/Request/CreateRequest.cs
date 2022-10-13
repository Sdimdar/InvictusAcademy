using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class CreateRequest : EndpointBaseAsync
    .WithRequest<CreateRequestCommand>
    .WithActionResult<DefaultResponceObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateRequest(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/Request/Create")]
    [SwaggerOperation(
        Summary = "Создание заявки",
        Description = "Необходимо передать в теле запроса поля",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult<DefaultResponceObject<string>>> HandleAsync(CreateRequestCommand request,
                                                                                        CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponceObject<string>>(response));
    }
}