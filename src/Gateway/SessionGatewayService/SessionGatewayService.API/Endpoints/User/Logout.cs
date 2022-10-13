using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using SessionGatewayService.API.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace SessionGatewayService.API.Endpoints.User;

public class Logout : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponceObject<string>>
{
    private readonly IMapper _mapper;

    public Logout(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost("User/Logout")]
    [SwaggerOperation(
        Summary = "Выход пользователя из системы",
        Description = "Сработает вне зависимости от того залогинен пользователь или нет",
        Tags = new[] { "User" })
    ]
    public async override Task<ActionResult<DefaultResponceObject<string>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        HttpContext.Session.Remove("user");
        return Ok(_mapper.Map<DefaultResponceObject<UserVm>>(Result.Success()));
    }
}
