using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionGatewayService.API.Extensions;
using SessionGatewayService.Application.Features.User.Commands.Login;
using SessionGatewayService.Domain;
using SessionGatewayService.Domain.Entities;

namespace SessionGatewayService.API.Endpoints.User;

public class Login : EndpointBaseAsync
    .WithRequest<LoginCommand>
    .WithActionResult<DefaultResponceObject<UserVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Login(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/Login")]
    public async override Task<ActionResult<DefaultResponceObject<UserVm>>> HandleAsync([FromBody] LoginCommand request,
                                                                                        CancellationToken cancellationToken = default)
    {
        var responce = await _mediator.Send(request, cancellationToken);
        if (responce.IsSuccess)
        {
            HttpContext.Session.SetData("user", new SessionData() { Email = request.Email });
            return Ok(_mapper.Map<DefaultResponceObject<UserVm>>(Result.Success()));
        }
        return Ok(_mapper.Map<DefaultResponceObject<UserVm>>(responce));
    }
}
