using Ardalis.ApiEndpoints;
using DataTransferLib.Models;
using Identity.API.Extensions;
using Identity.Application.Features.Users.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Login : EndpointBaseAsync
    .WithRequest<LoginQuerry>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;
    private readonly JWTSettings _jwtSettings;
    public Login(IMediator mediator, IOptions<JWTSettings> jwtSettings)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("/user/login")]
    [SwaggerOperation(
        Summary = "Авторизация пользователя",
        Description = "При авторизации пользователя вводятся его логин и пароль",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult> HandleAsync([FromBody] LoginQuerry request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        if (result.Item2.IsSuccess) HttpContext.Response.Cookies.SetJwtToken(result.Item1, _jwtSettings);
        return Ok(result.Item2);
    }
}
