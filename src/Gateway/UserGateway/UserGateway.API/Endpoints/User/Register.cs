using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Domain.Entities;

namespace UserGateway.API.Endpoints.User;

public class Register : EndpointBaseAsync
    .WithRequest<RegisterCommand>
    .WithActionResult<DefaultResponseObject<RegisterVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Register(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/Register")]
    [SwaggerOperation(
        Summary = "Регистрация нового пользователя",
        Description = "Необходимо передать в теле запроса необходимые поля",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<RegisterVm>>> HandleAsync(RegisterCommand request,
                                                                                      CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsSuccess) return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(response));
        HttpContext.Session.SetData("user", new SessionData() { Email = request.Email });
        return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(Result.Success()));
    }
}
