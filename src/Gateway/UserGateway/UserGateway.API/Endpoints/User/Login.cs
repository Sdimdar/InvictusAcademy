using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.User.Commands.Login;
using UserGateway.Domain.Entities;

namespace UserGateway.API.Endpoints.User;

public class Login : EndpointBaseAsync
    .WithRequest<LoginCommand>
    .WithActionResult<DefaultResponseObject<UserVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Login(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/Login")]
    [SwaggerOperation(
        Summary = "Вход пользователя в систему",
        Description = "Для входа пользователя необходимо ввести логин и пароль",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<UserVm>>> HandleAsync([FromBody] LoginCommand request,
                                                                                        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsSuccess) return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(response));
        HttpContext.Session.SetData("user", new SessionData() { Id = response.Value.Id,  Email = request.Email });
        return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(Result.Success()));
    }
}
