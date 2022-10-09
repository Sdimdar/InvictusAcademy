using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.Features.Requests.Commands.CreateRequest;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class CreateRequest : EndpointBaseAsync
    .WithRequest<CreateRequestCommand>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;

    public CreateRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/request/create")]
    [SwaggerOperation(
        Summary = "Создание заявки",
        Description = "Необходимо передать в теле запроса поля",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult> HandleAsync(CreateRequestCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}