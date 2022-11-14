using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using DataTransferLib.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;

namespace UserGateway.API.Endpoints.User;

public class Logout : EndpointBaseSync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<string>>
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
    public override ActionResult<DefaultResponseObject<string>> Handle()
    {
        HttpContext.Session.Remove("user");
        return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(Result.Success()));
    }
}
