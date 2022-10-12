using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using AutoMapper;
using DataTransferLib.Models;
using Identity.Application.Features.Users.Queries.GetUserData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionGatewayService.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.API.Endpoints.User;

public class GetUserData : EndpointBaseAsync
    .WithRequest<string>
    .WithResult<ActionResult>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUserData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/user/getUserData")]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных о пользователе необходимо передать его email через параметры в строке",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult> HandleAsync(string email, CancellationToken cancellationToken = default)
    {
        GetUserDataQuerry command = new(email);
        return Ok(_mapper.Map<DefaultResponceObject<UserVm>>(await _mediator.Send(command, cancellationToken)));
    }
}
