using Ardalis.ApiEndpoints;
using Ardalis.Result;
using AutoMapper;
using CommonStructures;
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
    private readonly ILogger<Register> _logger;

    public Register(IMediator mediator, IMapper mapper, ILogger<Register> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
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
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"City {request.City}" +
                               $"Email {request.Email}" +
                               $"FirstName {request.FirstName}" +
                               $"InstagramLink {request.InstagramLink}" +
                               $"PhoneNumber {request.PhoneNumber}" +
                               $"LastName {request.LastName}" +
                               $"MiddleName {request.MiddleName}");
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsSuccess) return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(response));
        HttpContext.Session.SetData("user", new SessionData() { Email = request.Email });
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" +
                               $"Errors {response.Errors}" +
                               $"ValidationErrors {response.ValidationErrors}" +
                               $"IsSuccess {response.IsSuccess}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<UserVm>>(Result.Success()));
    }
}
