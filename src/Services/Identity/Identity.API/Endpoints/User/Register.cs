using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using Identity.Application.Features.Users.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Register : EndpointBaseAsync
    .WithRequest<RegisterCommand>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;

    public Register(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
    }

    [HttpPost("/user/register")]
    [SwaggerOperation(
        Summary = "Регистрация нового пользователя",
        Description = "Необходимо передать в теле запроса необходимые поля",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult> HandleAsync(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        Result<RegisterCommandVm> responce = await _mediator.Send(request, cancellationToken);
        return Ok(responce);
    }
}
