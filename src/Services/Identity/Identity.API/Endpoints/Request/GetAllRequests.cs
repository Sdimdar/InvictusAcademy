using Ardalis.ApiEndpoints;
using Identity.Application.Features.Requests.Queries.GetAllRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.Request;

public class GetAllRequests:EndpointBaseAsync
    .WithRequest<GetAllRequestCommand>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;
    
    
    //надо добавить для возможность запроса, только для определенной роли
    public GetAllRequests(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/request/getall")]
    [SwaggerOperation(
        Summary = "Запрос на выгрузку всех заявок",
        Description = "Могут запрашивать только пользователи с ролью админ",
        Tags = new[] { "Request" })
    ]
    
    public override async Task<ActionResult> HandleAsync([FromQuery]GetAllRequestCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}