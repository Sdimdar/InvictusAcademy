using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Identity.Application.Features.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Login : EndpointBaseAsync
    .WithRequest<LoginQuerry>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;

    public Login(IMediator mediator)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
    }

    [HttpPost("/user/login")]
    [SwaggerOperation(
        Summary = "Авторизация пользователя",
        Description = "При авторизации пользователя вводятся его логин и пароль",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult> HandleAsync([FromBody] LoginQuerry request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response) ;
    }
}
