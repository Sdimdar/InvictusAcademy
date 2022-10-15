using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.Features.Requests.Queries.GetPagesCount;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class GetRequestsCount : EndpointBaseAsync
    .WithoutRequest
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;

    public GetRequestsCount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("request/Count")]
    [SwaggerOperation(
        Summary = "Запрос на получение количества заявок",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetRequestsCountQuerry(), cancellationToken));
    }
}
