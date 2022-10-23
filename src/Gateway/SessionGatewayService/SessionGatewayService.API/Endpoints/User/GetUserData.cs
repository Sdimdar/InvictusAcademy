using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using SessionGatewayService.API.Extensions;
using SessionGatewayService.Application.Features.User.Querries.GetUserData;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionGatewayService.API.Endpoints.User;

public class GetUserData : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<UserVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetUserData(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("User/GetUserData")]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "User" })
    ]
    public async override Task<ActionResult<DefaultResponseObject<UserVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            string email = HttpContext.Session.GetData("user")!.Email;
            GetUserDataQuerry querry = new() { Email = email };
            var response = await _mediator.Send(querry, cancellationToken);
            return Ok(_mapper.Map<DefaultResponseObject<GetUserDataVm>>(response));
        }
        catch (Exception)
        {
            return Ok(_mapper.Map<DefaultResponseObject<GetUserDataVm>>(Result.Error("User is not Autorized")));
        }
        
    }
}
