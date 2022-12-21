using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;
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
    private readonly ILogger<Login> _logger;

    public Login(IMediator mediator, IMapper mapper, ILogger<Login> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
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
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"Email {request.Email}");
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsSuccess) return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(response));
        HttpContext.Session.SetData("user", new SessionData() { Id = response.Value.Id, Email = request.Email });
        return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(Result.Success()));
    }
}
