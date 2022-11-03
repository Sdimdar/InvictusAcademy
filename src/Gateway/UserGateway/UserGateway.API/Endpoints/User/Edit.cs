﻿using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.User;

public class Edit : EndpointBaseAsync
    .WithRequest<EditCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Edit(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/Edit")]
    [SwaggerOperation(
        Summary = "Изменение данных пользователя",
        Description = "Для изменения данных пользователь должен быть залогинен, необходимо ввести данные в виде JSON",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody] EditCommand request,
                                                                                  CancellationToken cancellationToken = default)
    {
        request.Email = HttpContext.Session.GetData("user").Email;
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}