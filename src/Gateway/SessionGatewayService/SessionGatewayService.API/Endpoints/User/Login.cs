using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SessionGatewayService.API.Extensions;
using SessionGatewayService.Application.Features.User.Commands;

namespace SessionGatewayService.API.Endpoints.User;

public class Login : EndpointBaseAsync
    .WithRequest<LoginCommand>
    .WithActionResult
{
    private readonly IMediator _mediator;

    public Login(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async override Task<ActionResult> HandleAsync([FromBody] LoginCommand request, CancellationToken cancellationToken = default)
    {
        if (await _mediator.Send(request, cancellationToken))
        {
            HttpContext.Session.SetData("rararara", new Domain.SessionData() { SessionId = new Guid() });
            return Ok(true);
        }
        return Ok(false);
    }
}
