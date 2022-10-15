using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Request.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class ChangeCalledStatus : EndpointBaseAsync
    .WithRequest<ChangeCalledStatusCommand>
    .WithActionResult<DefaultResponceObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ChangeCalledStatus(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/request/calledStatus")]
    [SwaggerOperation(
        Summary = "Изменения статуса заявки",
        Description = "Необходимо передать id заявки",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult<DefaultResponceObject<string>>> HandleAsync(ChangeCalledStatusCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponceObject<string>>(response));
    }
}