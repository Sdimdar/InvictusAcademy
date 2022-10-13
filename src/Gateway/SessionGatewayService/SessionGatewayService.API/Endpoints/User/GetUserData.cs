using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SessionGatewayService.API.Extensions;
using SessionGatewayService.Application.Features.User.Querries.GetUserData;
using SessionGatewayService.Domain.ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionGatewayService.API.Endpoints.User;

public class GetUserData : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponceObject<UserVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUserData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("User/GetUserData")]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "User" })
    ]
    public async override Task<ActionResult<DefaultResponceObject<UserVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            string email = HttpContext.Session.GetData("user")!.Email;
            GetUserDataQuerry querry = new() { Email = email };
            var responce = await _mediator.Send(querry, cancellationToken);
            return Ok(_mapper.Map<DefaultResponceObject<GetUserDataVm>>(responce));
        }
        catch (Exception)
        {
            return Ok(_mapper.Map<DefaultResponceObject<GetUserDataVm>>(Result.Error("User is not Autorized")));
        }
        
    }
}
