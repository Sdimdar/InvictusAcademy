﻿using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using SessionGatewayService.API.Extensions;
using SessionGatewayService.Application.Features.User.Commands.Login;
using SessionGatewayService.Domain;
using SessionGatewayService.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionGatewayService.API.Endpoints.User;

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
    public async override Task<ActionResult<DefaultResponseObject<UserVm>>> HandleAsync([FromBody] LoginCommand request,
                                                                                        CancellationToken cancellationToken = default)
    {
        var Response = await _mediator.Send(request, cancellationToken);
        if (Response.IsSuccess)
        {
            HttpContext.Session.SetData("user", new SessionData() { Email = request.Email });
            return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(Result.Success()));
        }
        return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(Response));
    }
}
