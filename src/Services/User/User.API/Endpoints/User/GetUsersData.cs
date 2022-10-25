﻿using Ardalis.ApiEndpoints;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Queries;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace User.API.Endpoints.User;

public class GetUsersData : EndpointBaseAsync
    .WithRequest<GetUsersDataQuery>
    .WithActionResult<DefaultResponseObject<UsersVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUsersData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
    }

    [HttpGet("/User/GetUsersData")]
    [SwaggerOperation(
        Summary = "Получение данных пользователей",
        Description = "Для пагинации требуется вести в строку номер страницы, строка фильтра может быть пустой",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<UsersVm>>> HandleAsync([FromQuery] GetUsersDataQuery command,
                                                                                             CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<DefaultResponseObject<UsersVm>>(result));
    }
}