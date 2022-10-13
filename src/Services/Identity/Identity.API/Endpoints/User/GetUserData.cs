using Ardalis.ApiEndpoints;
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
    .WithActionResult<DefaultResponceObject<UserVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUserData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        _mapper = mapper ?? throw new NullReferenceException(nameof(mapper));
    }

    [HttpGet("/User/GetUserData")]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных о пользователе необходимо передать его email через параметры в строке",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponceObject<UserVm>>> HandleAsync(string email,
                                                                                        CancellationToken cancellationToken = default)
    {
        GetUserDataQuerry command = new(email);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<DefaultResponceObject<UserVm>>(result));
    }
}
