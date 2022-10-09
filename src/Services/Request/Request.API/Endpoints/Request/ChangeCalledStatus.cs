using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.Features.Requests.Commands.ChangeCalledStatus;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class ChangeCalledStatus:EndpointBaseAsync
    .WithRequest<ChangeCalledStatusCommand>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;


    public ChangeCalledStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/request/calledStatus")]
    [SwaggerOperation(
        Summary = "Изменения статуса заявки",
        Description = "Необходимо передать id заявки",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult> HandleAsync(ChangeCalledStatusCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }
}