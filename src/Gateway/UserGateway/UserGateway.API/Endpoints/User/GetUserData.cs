using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Responses;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;
using UserGateway.Application.Features.User.Querries.GetUserData;

namespace UserGateway.API.Endpoints.User;

public class GetUserData : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<DefaultResponseObject<UserVm>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<GetUserData> _logger;

    public GetUserData(IMediator mediator, IMapper mapper, ILogger<GetUserData> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("User/GetUserData")]
    [SwaggerOperation(
        Summary = "Получение данных о пользователе",
        Description = "Для получения данных пользователь должен быть залогинен",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<UserVm>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        string? email = HttpContext.Session.GetData("user")?.Email;
        GetUserDataQuerry query = new() { Email = email };
        var response = await _mediator.Send(query, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" +
                               $"Errors {response.Errors}" +
                               $"ValidationErrors {response.ValidationErrors}" +
                               $"IsSuccess {response.IsSuccess}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<GetUserDataVm>>(response));
    }
}
