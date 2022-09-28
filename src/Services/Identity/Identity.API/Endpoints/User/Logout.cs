using Ardalis.ApiEndpoints;
using Identity.Application.Features.Users.Commands.Logout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

    [HttpPost("/user/logout")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Деавторизация пользователя",
        Description = "Только для авторизованных пользователей",
        Tags = new[] { "User" })
    ]
    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new LogoutCommand(), cancellationToken);
    }
}
