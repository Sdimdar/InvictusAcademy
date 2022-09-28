using Ardalis.ApiEndpoints;
using Identity.Application.Features.Users.Commands.Logout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Endpoints.User;

public class Logout : EndpointBaseAsync
    .WithoutRequest
    .WithoutResult
{
    private readonly IMediator _mediator;

    public Logout(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("user/logout")]
    [Authorize]
    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new LogoutCommand(), cancellationToken);
    }
}
