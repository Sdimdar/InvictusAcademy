﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using Identity.Application.Features.Users.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionGatewayService.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class Register : EndpointBaseAsync
    .WithRequest<RegisterCommand>
    .WithActionResult<DefaultResponceObject<RegisterVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Register(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
    }

    [HttpPost("/User/Register")]
    [SwaggerOperation(
        Summary = "Регистрация нового пользователя",
        Description = "Необходимо передать в теле запроса необходимые поля",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponceObject<RegisterVm>>> HandleAsync([FromBody] RegisterCommand request,
                                                                                                   CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponceObject<RegisterVm>>(result));
    }
}
