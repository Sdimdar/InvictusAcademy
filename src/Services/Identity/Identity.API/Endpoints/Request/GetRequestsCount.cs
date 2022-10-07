using Ardalis.ApiEndpoints;
using Identity.Application.Features.Requests.Queries.GetPagesCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.Request;

public class GetRequestsCount : EndpointBaseAsync
    .WithoutRequest
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;

    public GetRequestsCount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Request/Count")]
    [SwaggerOperation(
        Summary = "Запрос на получение количества заявок",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "Request" })
    ]
    public async override Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetRequestsCountQuerry(), cancellationToken));
    }
}
