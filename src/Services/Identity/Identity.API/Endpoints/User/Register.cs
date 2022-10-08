using Ardalis.ApiEndpoints;
using DataTransferLib.Models;
using Identity.API.Extensions;
using Identity.Application.Features.Users.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Register : EndpointBaseAsync
    .WithRequest<RegisterCommand>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;
    private readonly JWTSettings _jwtSettings;

    public Register(IMediator mediator, IOptions<JWTSettings> jwtSettings)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("/user/register")]
    [SwaggerOperation(
        Summary = "Регистрация нового пользователя",
        Description = "Необходимо передать в теле запроса необходимые поля",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult> HandleAsync(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        if (result.Item2.IsSuccess) HttpContext.Response.Cookies.SetJwtToken(result.Item1, _jwtSettings);
        return Ok(result.Item2);
    }
}
