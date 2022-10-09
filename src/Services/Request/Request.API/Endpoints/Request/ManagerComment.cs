using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.Features.Requests.Commands.ManagerComment;
using Swashbuckle.AspNetCore.Annotations;

namespace Request.API.Endpoints.Request;

public class ManagerComment : EndpointBaseAsync
    .WithRequest<ManagerCommentCommand>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;

    public ManagerComment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("request/comment")]
    [SwaggerOperation(
        Summary = "Сохранение комментария для заявки",
        Description = "Необходимо передать id заявки и комментарий",
        Tags = new[] { "Request" })
    ]
    public override async Task<ActionResult> HandleAsync(ManagerCommentCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}