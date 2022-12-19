using Ardalis.ApiEndpoints;
using AutoMapper;
using CommonStructures;
using DataTransferLib.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Identity.Requests.Commands;
using Swashbuckle.AspNetCore.Annotations;
using UserGateway.API.Extensions;

namespace UserGateway.API.Endpoints.User;

public class EditPassword : EndpointBaseAsync
    .WithRequest<EditPasswordCommand>
    .WithActionResult<DefaultResponseObject<string>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<EditPassword> _logger;

    public EditPassword(IMediator mediator, IMapper mapper, ILogger<EditPassword> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("User/EditPassword")]
    [SwaggerOperation(
        Summary = "Изменение пароля пользователя",
        Description = "Для изменения данных пользователь должен быть залогинен, необходимо ввести данные в виде JSON",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<DefaultResponseObject<string>>> HandleAsync([FromBody] EditPasswordCommand request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"{BussinesErrors.ReceiveData.ToString()}" +
                               $"Email {request.Email}");
        request.Email = HttpContext.Session.GetData("user").Email;
        var response = await _mediator.Send(request, cancellationToken);
        _logger.LogInformation($"{BussinesErrors.ReturnData.ToString()}" +
                               $"Errors {response.Errors}" +
                               $"ValidationErrors {response.ValidationErrors}" +
                               $"IsSuccess {response.IsSuccess}" +
                               $"");
        return Ok(_mapper.Map<DefaultResponseObject<string>>(response));
    }
}